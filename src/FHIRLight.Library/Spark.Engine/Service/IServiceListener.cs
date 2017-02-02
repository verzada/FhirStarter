using System;
using FHIRLight.Library.Spark.Engine.Core;

namespace FHIRLight.Library.Spark.Engine.Service
{
    public interface IServiceListener
    {
        void Inform(Uri location, Entry interaction);
    }

}
