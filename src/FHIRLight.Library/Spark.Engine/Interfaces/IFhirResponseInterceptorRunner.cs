using System.Collections.Generic;
using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Interfaces
{
    public interface IFhirResponseInterceptorRunner
    {
        void AddInterceptor(IFhirResponseInterceptor interceptor);
        void ClearInterceptors();
        FhirResponse RunInterceptors(Entry entry, IEnumerable<object> parameters);
    }
}