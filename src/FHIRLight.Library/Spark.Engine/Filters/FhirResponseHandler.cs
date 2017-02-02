using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Filters
{
    public class FhirResponseHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                task =>
                {
                    if (task.IsCompleted)
                    {
                        FhirResponse fhirResponse;
                        if (task.Result.TryGetContentValue(out fhirResponse))
                        {
                            return request.CreateResponse(fhirResponse);
                        }
                        return task.Result;
                    }
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                    //return task.Result;
                }, 
                cancellationToken
            );
             
        }

    }

    
}
