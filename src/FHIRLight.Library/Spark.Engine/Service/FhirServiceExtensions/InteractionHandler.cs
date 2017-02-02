using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Service.FhirServiceExtensions
{
    public interface IInteractionHandler
    {
        FhirResponse HandleInteraction(Entry interaction);
    }
}