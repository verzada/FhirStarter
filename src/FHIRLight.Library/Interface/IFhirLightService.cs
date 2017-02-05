using System.Collections.Generic;
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

        FhirResponse Create(IKey key, Resource resource);
        FhirResponse Update(IKey key, Resource resource);
        FhirResponse Delete(IKey key);

        Conformance CreateMetaData();
    }
}
