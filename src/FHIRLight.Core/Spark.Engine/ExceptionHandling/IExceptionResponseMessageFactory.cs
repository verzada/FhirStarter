using System;
using System.Net.Http;

namespace FHIRLight.Core.Spark.Engine.ExceptionHandling
{
    public interface IExceptionResponseMessageFactory
    {
        HttpResponseMessage GetResponseMessage(Exception exception, HttpRequestMessage reques);
    }
}