using FHIRLight.Core.Parameters;
using Hl7.Fhir.Rest;

namespace FHIRLight.Server.Parameters
{
    public class PatientParameters :CommonParameters
    {
        public PatientParameters(SearchParams parameters) : base(parameters)
        {
        }
    }
}
