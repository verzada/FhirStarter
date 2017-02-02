using System.Collections.Generic;
using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Service
{
    public interface ITransfer
    {
        void Externalize(IEnumerable<Entry> interactions);
        void Externalize(Entry interaction);
        void Internalize(IEnumerable<Entry> interactions, Mapper<string, IKey> mapper);
        void Internalize(Entry entry);
    }
}