using FHIRLight.Library.Parameters;
using Hl7.Fhir.Rest;

namespace FHIRLight.Services.Parameters
{
    public class PatientParameters :CommonParameters
    {
        public PatientParameters(SearchParams parameters) : base(parameters)
        {
        }
    }
}
