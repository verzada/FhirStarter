/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/spark/master/LICENSE
 */

using System;
using System.Collections.Generic;
using FHIRLight.Library.Spark.Engine.Core;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

// mh: KeyExtensions terugverplaatst naar Spark.Engine.Core omdat ze in dezelfde namespace moeten zitten als Key.
namespace FHIRLight.Library.Spark.Engine.Extensions
{

    public static class KeyExtensions
    {
        public static Key ExtractKey(this Resource resource)
        {
            var _base = resource.ResourceBase != null ? resource.ResourceBase.ToString() : null;
            var key = new Key(_base, resource.TypeName, resource.Id, resource.VersionId);
            return key;
        }

        public static Key ExtractKey(Uri uri)
        {
            
            var identity = new ResourceIdentity(uri);
            
            var _base = identity.HasBaseUri ? identity.BaseUri.ToString() : null;
            var key = new Key(_base, identity.ResourceType, identity.Id, identity.VersionId);
            return key;
        }

        public static Key ExtractKey(this Localhost localhost, Bundle.EntryComponent entry)
        {
            var uri = new Uri(entry.Request.Url, UriKind.RelativeOrAbsolute);
          //  var compare = ExtractKey(uri); // This fails!! ResourceIdentity does not work in this case.
            return localhost.LocalUriToKey(uri);   
            
        }
                
        public static void ApplyTo(this IKey key, Resource resource)
        {
            resource.ResourceBase = key.HasBase() ?  new Uri(key.Base) : null;
            resource.Id = key.ResourceId;
            resource.VersionId = key.VersionId; 
        }

        public static Key Clone(this IKey self)
        {
            var key = new Key(self.Base, self.TypeName, self.ResourceId, self.VersionId);
            return key;
        }

        public static bool HasBase(this IKey key)
        {
            return !string.IsNullOrEmpty(key.Base);
        }

        public static Key WithBase(this IKey self, string _base)
        {
            var key = self.Clone();
            key.Base = _base;
            return key;
        }

        public static Key WithoutBase(this IKey self)
        {
            var key = self.Clone();
            key.Base = null;
            return key;
        }
        
        public static Key WithoutVersion(this IKey self)
        {
            var key = self.Clone();
            key.VersionId = null;
            return key;
        }

        public static bool HasVersionId(this IKey self)
        {
            return !string.IsNullOrEmpty(self.VersionId);
        }

        public static bool HasResourceId(this IKey self)
        {
            return !string.IsNullOrEmpty(self.ResourceId);
        }

        public static IEnumerable<string> GetSegments(this IKey key)
        {
            if (key.Base != null) yield return key.Base;
            if (key.TypeName != null) yield return key.TypeName;
            if (key.ResourceId != null) yield return key.ResourceId;
            if (key.VersionId != null)
            {
                yield return "_history";
                yield return key.VersionId;
            }
        }

        public static string ToUriString(this IKey key)
        {
            var segments = key.GetSegments();
            return string.Join("/", segments);
        }

        public static string ToOperationPath(this IKey self)
        {
            var key = self.WithoutBase();
            return key.ToUriString();
        }

        /// <summary>
        /// A storage key is a resource reference string that is ensured to be server wide unique.
        /// This way resource can refer to eachother at a database level.
        /// These references are also used in SearchResult lists.
        /// The format is "resource/id/_history/vid"
        /// </summary>
        /// <returns>a string</returns>
        public static string ToStorageKey(this IKey key)
        {
            return key.WithoutBase().ToUriString();
        }

        public static Key CreateFromLocalReference(string reference)
        {
            var parts = reference.Split('/');
            if (parts.Length == 2)
            {
                return Key.Create(parts[0], parts[1], parts[3]);
            }
            if (parts.Length == 4)
            {
                return Key.Create(parts[0], parts[1], parts[3]);
            }
            throw new ArgumentException("Could not create key from local-reference: " + reference);
        }

        public static Uri ToRelativeUri(this IKey key)
        {
            var path = key.ToOperationPath();
            return new Uri(path, UriKind.Relative);
        }

        public static Uri ToUri(this IKey self)
        {
            return new Uri(self.ToUriString(), UriKind.RelativeOrAbsolute);
        }

        public static Uri ToUri(this IKey key, Uri endpoint)
        {
            var _base = endpoint.ToString().TrimEnd('/');
            var s = $"{_base}/{key}";
            return new Uri(s);
        }


        /// <summary>
        /// Determines if the Key was constructed from a temporary id. 
        /// </summary>
        public static bool IsTemporary(this IKey key)
        {
            if (key.ResourceId != null)
            {
                return UriHelper.IsTemporaryUri(key.ResourceId);
            }
            return false;
        }

        /// <summary>
        /// Value equality for two IKey's
        /// </summary>
        /// <returns>true if all parts of of the keys are the same</returns>
        public static bool EqualTo(this IKey key, IKey other)
        {
            return key.Base == other.Base
                && key.TypeName == other.TypeName
                && key.ResourceId == other.ResourceId
                && key.VersionId == other.VersionId;
        }


    }
}
