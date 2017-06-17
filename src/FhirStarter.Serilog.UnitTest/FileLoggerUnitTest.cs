using NUnit.Framework;
using Serilog.Core;

namespace FhirStarter.Serilog.UnitTest
{
    [TestFixture]
    [Category("FileLogging")]
    public class FileLoggerUnitTest
    {
        private Logger logger;

        [SetUp]
        public void Setup()
        {
            logger = LoggerSerilog.GetLogger();
        }

        [Test]
        public void FileLoggingIsEnabled()
        {
            Assert.IsTrue(LoggerSerilog.IsFileLoggingEnabled(), "File logging is not enabled, check settings");
        }

        [Test]
        public void WriteToFile_Valid()
        {
            logger.Information("This is a test to write to file");
        }
    }
}
