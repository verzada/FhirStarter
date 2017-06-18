using NUnit.Framework;
using Serilog.Core;

namespace FhirStarter.Serilog.UnitTest
{
    [TestFixture]
    [Category("FileLogging")]
    public class FileLoggerUnitTest
    {
        private Logger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = LoggerSerilog.GetLogger();
        }

        [Test]
        public void FileLoggingIsEnabled()
        {
            Assert.IsTrue(LoggerSerilog.IsFileLoggingEnabled(), "File logging is not enabled, check settings");
        }

        [Test]
        public void WriteToFile_Valid()
        {
            _logger.Information("This is a test to write to file");
        }
    }
}
