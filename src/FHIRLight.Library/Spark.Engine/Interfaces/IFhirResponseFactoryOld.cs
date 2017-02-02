using System.Collections.Generic;
using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Interfaces
{
    public interface IFhirResponseFactoryOld
    {
        FhirResponse GetFhirResponse(Key key, IEnumerable<object> parameters =  null);
        FhirResponse GetFhirResponse(Entry entry, IEnumerable<object> parameters = null);
        FhirResponse GetFhirResponse(Key key, params object[] parameters);
        FhirResponse GetFhirResponse(Entry entry, params object[] parameters);

    }
}