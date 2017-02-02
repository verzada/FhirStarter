using System;
using System.Diagnostics;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace FHIRLight.Server
{
    public class UnityLogExtension : UnityContainerExtension, IBuilderStrategy
    {
        protected override void Initialize()
        {
            Debug.WriteLine("UnityLogExtension initializing");
            Context.Strategies.Add(this, UnityBuildStage.PreCreation);
        }

        void IBuilderStrategy.PostBuildUp(IBuilderContext context)
        {
            //Type type = context.Existing == null ? context.BuildKey.Type : context.Existing.GetType();
            //Debug.WriteLine("Builded up: " + type.Name);
        }

        void IBuilderStrategy.PostTearDown(IBuilderContext context)
        {
        }

        void IBuilderStrategy.PreBuildUp(IBuilderContext context)
        {
            Type type = context.Existing?.GetType() ?? context.BuildKey.Type;
            Debug.WriteLine("Building up: " + type.Name);
        }

        void IBuilderStrategy.PreTearDown(IBuilderContext context)
        {
        }
    }

}