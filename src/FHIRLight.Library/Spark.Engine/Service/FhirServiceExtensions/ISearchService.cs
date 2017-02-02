using FHIRLight.Library.Spark.Engine.Core;
using Hl7.Fhir.Rest;
using Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Service.FhirServiceExtensions
{
    public interface ISearchService : IFhirServiceExtension
    {
        Snapshot GetSnapshot(string type, SearchParams searchCommand);
        Snapshot GetSnapshotForEverything(IKey key);
        IKey FindSingle(string type, SearchParams searchCommand);

        IKey FindSingleOrDefault(string type, SearchParams searchCommand);
        SearchResults GetSearchResults(string type, SearchParams searchCommand);
    }
}