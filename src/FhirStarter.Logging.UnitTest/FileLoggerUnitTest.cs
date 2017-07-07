using log4net.Repository.Hierarchy;
using NUnit.Framework;

namespace FhirStarter.Serilog.UnitTest
{
    // Test with curl:
    // curl -k -u "x:<token>" http://<hostname>:8088/services/collector/event -d "{\"event\": \"basic auth ftw!\"}"

    [TestFixture]
    [Category("FileLogging")]
    public class FileLoggerUnitTest
    {
        private Logger _logger;

        [SetUp]
        public void Setup()
        {
_logger = new RootLogger();
        }

        [Test]
        public void FileLoggingIsEnabled()
        {
            Assert.IsTrue(SetupSerilogLogging.IsFileLoggingEnabled(), "File logging is not enabled, check settings");
        }

        [Test]
        public void WriteToFile_Valid()
        {
            _logger.Error("This is a test to write to file");
        }
    }
}
