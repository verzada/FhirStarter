using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using System.Xml.Linq;
using FhirStarter.Bonfire.Log;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace FhirStarter.Bonfire.Filters
{
    public abstract class AbstractExceptionFilter: ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext context)
        {
            var exceptionType = context.Exception.GetType();
            var expectedType = GetExceptionType();
            if (exceptionType != expectedType && !(expectedType == typeof(Exception))) return;
            var outCome = GetOperationOutCome(context.Exception);

            
            var xml = FhirSerializer.SerializeResourceToXml(outCome);
            var xmlDoc = XDocument.Parse(xml);
            var error = xmlDoc.ToString();
            var requestUrl = string.Empty;
            var logger = SetupSerilogLogging.GetLogger();


            if (context.Request != null && context.Request.RequestUri != null)
            {
                requestUrl = context.Request.RequestUri.AbsoluteUri;
            }

            if (!string.IsNullOrEmpty(requestUrl) && logger != null)
            {
                var strBuilder = new StringBuilder();
                strBuilder.AppendLine();
                strBuilder.AppendLine("RequestUrl: " + requestUrl);
                strBuilder.AppendLine("ErrorMessage: ");
                strBuilder.AppendLine(error);
                logger.Error(strBuilder.ToString());
            }
            else
            {
                logger?.Error(error);
            }

            context.Response = new HttpResponseMessage
            {                
                Content = new StringContent(error, Encoding.UTF8, "application/xml"),
                StatusCode = HttpStatusCode.InternalServerError
            };
            
        }

        protected abstract Resource GetOperationOutCome(Exception exception);

        protected abstract Type GetExceptionType();
    }
}