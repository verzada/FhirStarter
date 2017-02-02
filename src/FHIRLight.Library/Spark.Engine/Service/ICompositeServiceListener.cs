using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Service
{
    public interface ICompositeServiceListener : IServiceListener
    {
        void Add(IServiceListener listener);
        void Clear();
        void Inform(Entry interaction);
    }
}