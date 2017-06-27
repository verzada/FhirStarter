using System;
using FhirStarter.Logging.Common;
using FhirStarter.Logging.Interface;
using Hl7.Fhir.Model;
using Serilog.Core;

namespace FhirStarter.Logging.Serilog
{
   public class LogWithSerilog:ILoggingFramework
   {
       private Logger _logger;

       public LogWithSerilog()
       {
           _logger = SetupSerilogLogging.GetLogger();
       }


       private void LogByType(LoggingType type, string message, string messageTemplate, string exceptionMessageTemplate,
           Exception exception)
       {
           switch (type)
           {
               case LoggingType.Info:
                   if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(messageTemplate))
                   {
                       _logger.Information(message);
                   }
                   if (!string.IsNullOrEmpty(exceptionMessageTemplate) && exception != null)
                   {
                       _logger.Information(exception, exceptionMessageTemplate);
                   }
                   break;
               case LoggingType.Debug:
                   if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(messageTemplate))
                   {
                       _logger.Debug(message);
                   }
                   if (!string.IsNullOrEmpty(exceptionMessageTemplate) && exception != null)
                   {
                       _logger.Debug(exception, exceptionMessageTemplate);
                   }
                   break;
               case LoggingType.Warning:
                   if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(messageTemplate))
                   {
                       _logger.Warning(message);
                   }
                   if (!string.IsNullOrEmpty(exceptionMessageTemplate) && exception != null)
                   {
                       _logger.Warning(exception, exceptionMessageTemplate);
                   }
                   break;
               case LoggingType.Error:
                   if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(messageTemplate))
                   {
                       _logger.Error(message);
                   }
                   if (!string.IsNullOrEmpty(exceptionMessageTemplate) && exception != null)
                   {
                       _logger.Error(exception, exceptionMessageTemplate);
                   }
                   break;
               case LoggingType.Fatal:
                   if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(messageTemplate))
                   {
                       _logger.Fatal(message);
                   }
                   if (!string.IsNullOrEmpty(exceptionMessageTemplate) && exception != null)
                   {
                       _logger.Fatal(exception, exceptionMessageTemplate);
                   }
                   break;
           }
       }

       public void LogOperationOutcome(OperationOutcome outcome, string message = null)
       {
           throw new NotImplementedException();
       }

       public void LogException(Exception exception, string message = null)
       {
           throw new NotImplementedException();
       }

       public void LogError(string message, Exception exception = null)
       {
           throw new NotImplementedException();
       }

       public void LogInfo(string message)
       {
           if (!string.IsNullOrEmpty(message))
           {
               _logger.Information(message);
           }
       }

       public void LogByType(string message, Exception exception, LoggingType logType)
       {
           throw new NotImplementedException();
       }
   }
}
