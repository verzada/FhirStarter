using System.Collections.Generic;
using FHIRLight.Library.Spark.Engine.Core;
using FHIRLight.Library.Spark.Engine.Interfaces;
using Hl7.Fhir.Model;

namespace FHIRLight.Library.Spark.Engine.Extensions
{
    public static class GeneratorKeyExtensions
    {
        public static Key NextHistoryKey(this IGenerator generator, IKey key)
        {
            Key historykey = key.Clone();
            historykey.VersionId = generator.NextVersionId(key.TypeName, key.ResourceId);
            return historykey;
        }

        public static Key NextKey(this IGenerator generator, Resource resource)
        {
            string resourceid = generator.NextResourceId(resource);
            Key key = resource.ExtractKey();
            string versionid = generator.NextVersionId(key.TypeName, key.ResourceId);
            return Key.Create(key.TypeName, resourceid, versionid);
        }

        public static void AddHistoryKeys(this IGenerator generator, List<Entry> entries)
        {
            // PERF: this needs a performance improvement.
            foreach (Entry entry in entries)
            {
                entry.Key = generator.NextHistoryKey(entry.Key);
            }
        }
    }
}
