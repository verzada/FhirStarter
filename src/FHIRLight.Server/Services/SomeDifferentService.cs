using System;
using System.Collections.Generic;
using System.Net.Http;
using FHIRLight.Library.Interface;
using FHIRLight.Library.Spark.Engine.Core;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace FHIRLight.Server.Services
{
    public class SomeDifferentService:IFhirLightService
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

        public HttpResponseMessage Create(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Update(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Delete(IKey key)
        {
            throw new NotImplementedException();
        }

        public Conformance CreateMetaData()
        {
            throw new NotImplementedException();
        }
    }
}
