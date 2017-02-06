using System;
using FHIRLight.Library.Interface;
using FHIRLight.Server.Services;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace FHIRLight.Server.UnitTests.Service
{
    [TestFixture]
   public class TestExamplePatientService
    {
        private IFhirLightService _patientService;

        [SetUp]
        public void Setup()
        {
            _patientService = new ExamplePatientService();
        }

        [Test]
        public void GetMetaData_CanaryTest_True()
        {
            var metaData = _patientService.CreateMetaData();

            Assert.IsTrue(metaData != null, "Should not be empty");
        }

        [Test]
        public void GetPatientResult_True()
        {
            var result = _patientService.Read("1");

            Assert.IsTrue(result != null, "Result should not be null");
            Assert.IsTrue(result.TypeName == nameof(Patient), "Expected a " + nameof(Patient) + " resource");
        }

        [Test]
        public void GetPatient_OperationOutcome_Exception()
        {
            var searchParams = new SearchParams();
            searchParams.Add("Given", "Name");

            //Act
            ActualValueDelegate<object> testDelegate = () =>_patientService.Read(searchParams);

            //Assert
            Assert.That(testDelegate, Throws.TypeOf<ArgumentException>());

        }
       
    }
}
