using System;
using FhirStarter.Logging.Common;
using Hl7.Fhir.Model;

namespace FhirStarter.Logging.Interface
{
    public interface ILoggingFramework
    {
        void LogOperationOutcome(OperationOutcome outcome, string message = null);

        void LogException(Exception exception, string message = null);

        void LogError(string message, Exception exception = null);

        void LogInfo(string message);

        void LogByType(string message, Exception exception, LoggingType logType);
    }
}
