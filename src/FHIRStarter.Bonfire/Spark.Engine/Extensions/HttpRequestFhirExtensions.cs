/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/spark/master/LICENSE
 */

using System.Net.Http;
using FhirStarter.Bonfire.Spark.Engine.Core;
using FhirStarter.Bonfire.Spark.Engine.Service;
using Hl7.Fhir.Rest;

namespace FhirStarter.Bonfire.Spark.Engine.Extensions
{
    public static class HttpRequestFhirExtensions
    {
        public static Entry GetEntry(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(Const.RESOURCE_ENTRY))
                return request.Properties[Const.RESOURCE_ENTRY] as Entry;
            return null;
        }

        public static SummaryType RequestSummary(this HttpRequestMessage request)
        {
            return request.GetParameter("_summary") == "true" ? SummaryType.True : SummaryType.False;
        }
    }
}