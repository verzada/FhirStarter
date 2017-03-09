using System.Collections.Generic;
using System.Net.Http;
using FhirStarter.Bonfire.Interface;
using FhirStarter.Bonfire.Parameters;
using FhirStarter.Bonfire.Spark.Engine.Core;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace FhirStarter.Inferno.Services
{
    public class ExamplePatientService : IFhirService
    {

        public ExamplePatientService()
        {
            
        }
        public List<string> GetSupportedResources()
        {
            return new List<string> {nameof(Patient)};
        }

        public string GetAlias()
        {
            return nameof(Patient);
        }

        public List<ModelInfo.SearchParamDefinition> SearchParameters()
        {
            return new List<ModelInfo.SearchParamDefinition>
            {
                new ModelInfo.SearchParamDefinition
                {
                    Resource = nameof(Patient),
                    Type = SearchParamType.Number,
                    Name = CommonParameters.ParameterIdentifier,
                    Description = "The patient social security number"
                }
            };
        }

        public OperationDefinition GetOperationDefinition()
        {
            return new OperationDefinition();
        }

        public Base Read(SearchParams searchParams)
        {
            throw new System.ArgumentException("Using " + nameof(SearchParams) +
                                        " in Read(SearchParams searchParams) should throw an exception which is put into an OperationOutcomes issues");
        }

        private static Base MockPatient()
        {
            var date = new FhirDateTime(System.DateTime.Now);

            return new Patient
            {
                Meta = new Meta { LastUpdated = date.ToDateTimeOffset()},
                Id = "12345678901",
                Active = true,
                Name =
                    new List<HumanName>
                    {
                        new HumanName {Family = new List<string> {"Normann"}, Given = new List<string> {"Ola"}}
                    },
                Telecom =
                    new List<ContactPoint>
                    {
                        new ContactPoint {System = ContactPoint.ContactPointSystem.Phone, Value = "123467890"}
                    },
                Gender = AdministrativeGender.Male,
                BirthDate = "2000-01-01"
                
            };
        }

        public Base Read(string id)
        {
            return MockPatient();
        }

        public HttpResponseMessage Create(IKey key, Resource resource)
        {
            throw new System.NotImplementedException();
        }

        public HttpResponseMessage Update(IKey key, Resource resource)
        {
            throw new System.NotImplementedException();
        }

        public HttpResponseMessage Delete(IKey key)
        {
            throw new System.NotImplementedException();
        }

        public Conformance CreateMetaData()
        {
            throw new System.NotImplementedException();
        }
    }
}
