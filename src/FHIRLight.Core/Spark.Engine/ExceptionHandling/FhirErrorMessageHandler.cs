using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FHIRLight.Core.Spark.Engine.Extensions;
using Hl7.Fhir.Model;

namespace FHIRLight.Core.Spark.Engine.ExceptionHandling
{
    public class FhirErrorMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response =  await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var content = response.Content as ObjectContent;
                if (content != null && content.ObjectType == typeof (HttpError))
                {
                    var outcome = new OperationOutcome().AddError(response.ReasonPhrase);
                    return request.CreateResponse(response.StatusCode, outcome);
                }
            }
            return response;
        }
    }
}