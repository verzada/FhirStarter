using System.Collections.Generic;
using System.Net.Http;
using FhirStarter.Bonfire.Spark.Engine.Core;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace FhirStarter.Bonfire.Interface
{
    /// <summary>
    /// The interface used by the FhirStarter Inferno server to expose the fhir service. 
    /// It is not meant to handle internal services
    /// </summary>
    public interface IFhirService
    {
        // List the supported resource f.ex. Patient, Bundle etc
        List<string> GetSupportedResources();

        // The name of the Resource you can query
        string GetAlias();

        // Define conformance
        List<ModelInfo.SearchParamDefinition> SearchParameters();

        OperationDefinition GetOperationDefinition();

        // CRUD
        HttpResponseMessage Create(IKey key, Resource resource);
        Base Read(SearchParams searchParams);
        Base Read(string id);
        HttpResponseMessage Update(IKey key, Resource resource);
        HttpResponseMessage Delete(IKey key);
    }
}
