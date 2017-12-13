using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using System.Xml.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.Web;
using FhirStarter.Bonfire.Spark.Engine.Core;
using Newtonsoft.Json;

namespace FhirStarter.Bonfire.Filters
{
    public abstract class AbstractExceptionFilter: ExceptionFilterAttribute
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext context)
        {
            var exceptionType = context.Exception.GetType();
            var expectedType = GetExceptionType();
            if (exceptionType != expectedType && !(expectedType == typeof(Exception))) return;
            var outCome = GetOperationOutCome(context.Exception);

            var fhirXmlSerializer = new FhirXmlSerializer();
            var xml = fhirXmlSerializer.SerializeToString(outCome);
            var xmlDoc = XDocument.Parse(xml);
            var error = xmlDoc.ToString();
            var htmlDecode = WebUtility.HtmlDecode(error);
            Log.Error(htmlDecode);
            SetResponseForClient(context, outCome);
            
        }

        private static void SetResponseForClient(HttpActionExecutedContext context, Resource outCome)
        {
            // "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"
            var acceptEntry = HttpContext.Current.Request.Headers["Accept"];
            var acceptJson = acceptEntry.Contains(FhirMediaType.HeaderTypeJson);

            if (acceptJson)
            {
                var fhirJsonSerializer = new FhirJsonSerializer();
                var json = fhirJsonSerializer.SerializeToString(outCome);
                context.Response = new HttpResponseMessage
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json"),
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            else
            {
                var fhirXmlSerializer = new FhirXmlSerializer();
                var xml = fhirXmlSerializer.SerializeToString(outCome);
                context.Response = new HttpResponseMessage
                {
                    Content = new StringContent(xml, Encoding.UTF8, "application/xml"),
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        protected abstract Resource GetOperationOutCome(Exception exception);

        protected abstract Type GetExceptionType();
    }
}