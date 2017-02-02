using System.Collections.Generic;
using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Service.FhirServiceExtensions
{
    public interface IResourceStorageService : IFhirServiceExtension
    {
        Entry Get(IKey key);
        Entry Add(Entry entry);
        IList<Entry> Get(IEnumerable<string> localIdentifiers, string sortby = null);
    }
}