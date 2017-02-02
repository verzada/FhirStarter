using Hl7.Fhir.Model;
using System.Net;

namespace FHIRLight.Library.Spark.Engine.Core
{
    public class FhirResponse
    {
        public HttpStatusCode StatusCode;
        public IKey Key;
        public Resource Resource;

        public FhirResponse(HttpStatusCode code, IKey key, Resource resource)
        {
            StatusCode = code;
            this.Key = key;
            this.Resource = resource;
        }

        public FhirResponse(HttpStatusCode code, Resource resource)
        {
            this.StatusCode = code;
            this.Key = null;
            this.Resource = resource;
        }

        public FhirResponse(HttpStatusCode code)
        {
            this.StatusCode = code;
            this.Key = null;
            this.Resource = null;
        }

        public bool IsValid
        {
            get
            {
                int code = (int)this.StatusCode;
                return code <= 300;
            }
        }

        public bool HasBody
        {
            get
            {
                return Resource != null;
            }
        }

    }
}
