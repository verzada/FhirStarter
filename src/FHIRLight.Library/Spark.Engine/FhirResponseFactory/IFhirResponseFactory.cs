using System.Collections.Generic;
using FHIRLight.Library.Spark.Engine.Core;
using Hl7.Fhir.Model;

namespace FHIRLight.Library.Spark.Engine.FhirResponseFactory
{
    public interface IFhirResponseFactory
    {
        FhirResponse GetFhirResponse(Entry entry, Key key = null, params object[] parameters);
        FhirResponse GetFhirResponse(Entry entry, Key key = null, IEnumerable<object> parameters = null);
        FhirResponse GetMetadataResponse(Entry entry, Key key = null);

        FhirResponse GetFhirResponse(IList<Entry> interactions, Bundle.BundleType bundleType);

        FhirResponse GetFhirResponse(Bundle bundle);
    }
}