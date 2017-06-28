using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace FhirStarter.Bonfire.Log
{
    public static class ExceptionLogger
    {
        //http://stackoverflow.com/questions/1091853/error-message-unable-to-load-one-or-more-of-the-requested-types-retrieve-the-l
        public static void LogReflectionTypeLoadException(ReflectionTypeLoadException ex)
        {
            var sb = new StringBuilder();
            foreach (var exSub in ex.LoaderExceptions)
            {
                sb.AppendLine(exSub.Message);
                var exFileNotFound = exSub as FileNotFoundException;
                if (!string.IsNullOrEmpty(exFileNotFound?.FusionLog))
                {
                    sb.AppendLine("Fusion Log:");
                    sb.AppendLine(exFileNotFound.FusionLog);
                }
                sb.AppendLine();
            }
            var errorMessage = sb.ToString();
            var logger = SetupSerilogLogging.GetLogger();
            logger?.Error(errorMessage);

            Console.WriteLine(errorMessage);
        }
    }
}
