using System;

namespace FHIRLight.Library.Spark.Engine.Core
{
    public class Localhost : ILocalhost
    {
        public Uri DefaultBase { get; set; }

        public Localhost(Uri baseuri)
        {
            DefaultBase = baseuri;
        }

        public Uri Absolute(Uri uri)
        {
            if (uri.IsAbsoluteUri) 
            {
                return uri;
            }
            var _base = DefaultBase.ToString().TrimEnd('/') + "/";
            return new Uri(_base + uri);
        }

        public bool IsBaseOf(Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                var isbase = DefaultBase.Bugfixed_IsBaseOf(uri);
                return isbase;
            }
            return false;
        }

        public Uri GetBaseOf(Uri uri)
        {
            return (IsBaseOf(uri)) ? DefaultBase : null;
        }
    }

    
}
