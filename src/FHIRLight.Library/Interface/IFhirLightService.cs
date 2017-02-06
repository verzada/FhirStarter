using System.Collections.Generic;
using System.Net.Http;
using FHIRLight.Library.Spark.Engine.Core;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace FHIRLight.Library.Interface
{
    public interface IFhirLightService
    {
        List<string> GetSupportedResources();

        string GetAlias();

        List<ModelInfo.SearchParamDefinition> SearchParameters();

        OperationDefinition GetOperationDefinition();

        Base Read(SearchParams searchParams);

        Base Read(string id);

        HttpResponseMessage Create(IKey key, Resource resource);
        HttpResponseMessage Update(IKey key, Resource resource);
        HttpResponseMessage Delete(IKey key);

        Conformance CreateMetaData();
    }
}
