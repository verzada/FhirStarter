using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using FHIRLight.Library.Interface;
using FHIRLight.Library.Spark.Engine.Extensions;
using FHIRLight.Library.Spark.Engine.Infrastructure;
using FHIRLight.Services.Service;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace FHIRLight.Server.Controllers
{
    [RoutePrefix("fhir"), EnableCors("*", "*", "*", "*")]
    [RouteDataValuesOnly]
    public class FhirController : ApiController
    {
        private IFhirLightService lightService;

        public FhirController()
        {
            lightService = new PatientService();
        }

        //public FhirController(ICollection<IFhirService> services )
        //{
        //    _services = services;
        //}

        [HttpGet, Route("{type}/{id}"), Route("{type}/identifier/{id}")]
        public HttpResponseMessage Read(string type, string id)
        {
            //var result = _interpreter.ResourceQuery(type, id);

            //return SendResponse(result);
            var result = lightService.Read(id);
            
            return SendResponse(result);
        }

        [HttpGet, Route("{type}")]
        public HttpResponseMessage Read(string type)
        {
            var parameters = Request.GetSearchParams();
            if (parameters.Count > 0)
            {
                var results = lightService.Read(parameters);
                return SendResponse(results);
            }

          return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
        }

        [HttpGet, Route("")]
        // ReSharper disable once InconsistentNaming
        public HttpResponseMessage Query(string _query)
        {
            return new HttpResponseMessage();
        }

        [HttpPost, Route("{type}")]
        public HttpResponseMessage Create(string type, Resource resource)
        {
            return new HttpResponseMessage(HttpStatusCode.Ambiguous);
        }

        [HttpPut, Route("{type}/{id}")]
        public HttpResponseMessage Update(string type, string id, Resource resource)
        {
            //if (!string.IsNullOrEmpty(type) && resource != null && !string.IsNullOrEmpty(id))
            //{
            //    var key = Key.Create(type);
            //    var result = _interpreter.ResourceUpdate(type, key, resource);
            //    if (result != null)
            //        return result;
            //}
            return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
        }

        [HttpDelete, Route("{type}/{id}")]
        public HttpResponseMessage Delete(string type, string id)
        {
            //Key key = Key.Create(type, id);
            //var response = _interpreter.ResourceDelete(type, key);
            //return response;
            return  new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
        }

        private HttpResponseMessage SendResponse(Base resource)
        {
            //var validationResult = _validator.Validate(resource);
            //if (validationResult.Issue.Count != 0)
            //{
            //    resource = validationResult;
            //}
            var headers = Request.Headers;
            var accept = headers.Accept;
            var returnJson = accept.Any(x => x.MediaType.Contains("json"));

            StringContent httpContent;
            if (!returnJson)
            {
                var xml = FhirSerializer.SerializeToXml(resource);
                //#if DEBUG
                //                var xmlDocument = new XmlDocument();
                //                xmlDocument.LoadXml(xml);
                //                xmlDocument.Save(@"c:\temp\arntzen3.xml");
                //#endif
                httpContent =
                    new StringContent(xml, Encoding.UTF8,
                        "application/xml");

            }
            else
            {
                httpContent =
                    new StringContent(FhirSerializer.SerializeToJson(resource), Encoding.UTF8,
                        "application/json");
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = httpContent };
            return response;
        }

        //[HttpGet, Route("metadata")]
//        public HttpResponseMessage MetaData()
//        {
////            return new HttpResponseMessage(HttpStatusCode.Ambiguous);
            
//            var headers = Request.Headers;
//            var accept = headers.Accept;
//            var returnJson = accept.Any(x => x.MediaType.Contains("json"));

//            StringContent httpContent;
//            //var metaData = lightService.;
//            if (!returnJson)
//            {
//                var xml = FhirSerializer.SerializeToXml(metaData);              
//                httpContent =
//                    new StringContent(xml, Encoding.UTF8,
//                        "application/xml");

//            }
//            else
//            {
//                httpContent =
//                    new StringContent(FhirSerializer.SerializeToJson(metaData), Encoding.UTF8,
//                        "application/json");
//            }
//            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = httpContent };
//            return response; 
//        }
    }
}
