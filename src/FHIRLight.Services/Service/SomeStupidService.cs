using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FHIRLight.Library.Interface;
using FHIRLight.Library.Spark.Engine.Core;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace FHIRLight.Services.Service
{
    public class SomeStupidService:IFhirLightService
    {
        public List<string> GetSupportedResources()
        {
            throw new NotImplementedException();
        }

        public string GetAlias()
        {
            throw new NotImplementedException();
        }

        public List<ModelInfo.SearchParamDefinition> SearchParameters()
        {
            throw new NotImplementedException();
        }

        public OperationDefinition GetOperationDefinition()
        {
            throw new NotImplementedException();
        }

        public Base Read(SearchParams searchParams)
        {
            throw new NotImplementedException();
        }

        public Base Read(string id)
        {
            throw new NotImplementedException();
        }

        public FhirResponse Create(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public FhirResponse Update(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public FhirResponse Delete(IKey key)
        {
            throw new NotImplementedException();
        }

        public Conformance CreateMetaData()
        {
            throw new NotImplementedException();
        }
    }
}
