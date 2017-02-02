using System;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;

namespace FHIRLight.Library.Spark.Engine.App_Start
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
            Type type = context.Existing == null ? context.BuildKey.Type : context.Existing.GetType();
            Debug.WriteLine("Building up: " + type.Name);
        }

        void IBuilderStrategy.PreTearDown(IBuilderContext context)
        {
        }
    }

}