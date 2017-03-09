using System;
using System.Collections.Generic;
using System.Linq;
using FhirStarter.Bonfire.Interface;
using FhirStarter.Bonfire.Spark.Engine.Service.FhirServiceExtensions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

namespace FhirStarter.Bonfire.Service
{
    public static class ConformanceBuilderFhirStarter
    {
        public static Conformance AddUsedResources(this Conformance conformance,
            IEnumerable<IFhirService> services, bool readhistory, bool updatecreate,
            Conformance.ResourceVersionPolicy versioning)
        {
            var totalAvailableResources = new List<string>();
            foreach (var service in services)
            {
                var resourcesService = service;
                if (resourcesService != null)
                {
                    totalAvailableResources.AddRange(resourcesService.GetSupportedResources());
                }
            }

            foreach (var resource in totalAvailableResources)
            {
                conformance.AddSingleResourceComponent(resource, readhistory, updatecreate, versioning);
            }
            return conformance;
        }


        public static Conformance AddSearchSetInteraction(this Conformance conformance)
        {
            var searchSet = Conformance.SystemRestfulInteraction.SearchSystem;
            conformance.AddSystemInteraction(searchSet);

            return conformance;
        }


        public static Conformance AddSingleResourceComponent(this Conformance conformance, string resourcetype,
            bool readhistory, bool updatecreate, Conformance.ResourceVersionPolicy versioning,
            ResourceReference profile = null)
        {
            var resource = new Conformance.ResourceComponent
            {
                Type = (ResourceType)Enum.Parse(typeof(ResourceType), resourcetype, true),
                Profile = profile,
                ReadHistory = readhistory,
                UpdateCreate = updatecreate,
                Versioning = versioning
            };

            conformance.Server().Resource.Add(resource);
            return conformance;
        }

        public static Conformance AddOperationDefintion(this Conformance conformance, IEnumerable<IFhirService> services)
        {
            var operationComponents = new List<Conformance.OperationComponent>();

            foreach (var service in services)
            {
                var queryService = service;
                var operationDefintion = queryService?.GetOperationDefinition();
                if (!string.IsNullOrEmpty(operationDefintion?.Url))
                    operationComponents.Add(new Conformance.OperationComponent
                    {
                        Name = operationDefintion.Name,
                        Definition = new ResourceReference {Reference = operationDefintion.Url}
                    });
            }
            if (operationComponents.Count > 0)
                conformance.Server().Operation.AddRange(operationComponents);
            return conformance;
        }

        public static Conformance AddSearchTypeInteractionForResources(this Conformance conformance)
        {
            var firstOrDefault = conformance.Rest.FirstOrDefault();
            if (firstOrDefault != null)
                foreach (var r in firstOrDefault.Resource.ToList())
                {
                    conformance.Rest().Resource.Remove(r);
                    conformance.Rest().Resource.Add(AddSearchType(r));
                }
            return conformance;
        }

        public static Conformance.ResourceComponent AddSearchType(Conformance.ResourceComponent resourcecomp)
        {
            var type = Conformance.TypeRestfulInteraction.SearchType;
            var interaction = ConformanceBuilder.AddSingleResourceInteraction(resourcecomp, type);
            resourcecomp.Interaction.Add(interaction);
            return resourcecomp;
        }

        public static Conformance AddCoreSearchParamsAllResources(this Conformance conformance,
            IEnumerable<IFhirService> services)
        {
            var fhirStarterServices = services as IFhirService[] ?? services.ToArray();
            var firstOrDefault = conformance.Rest.FirstOrDefault();
            if (firstOrDefault != null)
                foreach (var r in firstOrDefault.Resource.ToList())
                {
                    foreach (var service in fhirStarterServices)
                    {
                        var resourceService = service;
                        if (resourceService != null)
                        {
                            conformance.Rest().Resource.Remove(r);
                            conformance.Rest().Resource.Add(AddCoreSearchParamsResource(r, resourceService.SearchParameters()));
                        }
                    }
                }
            return conformance;
        }


        public static Conformance.ResourceComponent AddCoreSearchParamsResource(Conformance.ResourceComponent r,
            IEnumerable<ModelInfo.SearchParamDefinition> availableModelInfo)
        {
            if (availableModelInfo != null)
            {
                var parameters =
                    availableModelInfo.Where(sp => sp.Resource == r.Type.GetLiteral())
                        .Select(sp => new Conformance.SearchParamComponent
                        {
                            Name = sp.Name,
                            Type = sp.Type,
                            Documentation = sp.Description
                        });

                r.SearchParam.AddRange(parameters);
            }
            return r;
        }

        public static Conformance CreateServer(string server, string publisher, string fhirVersion)
        {
            var conformance = new Conformance
            {
                Name = server,
                Publisher = publisher,
                FhirVersion = fhirVersion,
                AcceptUnknown = Conformance.UnknownContentCode.No,
                Date = Date.Today().Value,
                Kind = Conformance.ConformanceStatementKind.Instance
            };
            conformance.AddServer();
            return conformance;
            //AddRestComponent(true);
            //AddAllCoreResources(true, true, Conformance.ResourceVersionPolicy.VersionedUpdate);
            //AddAllSystemInteractions();
            //AddAllResourceInteractionsAllResources();
            //AddCoreSearchParamsAllResources();

            //return con;
        }
    }
}
