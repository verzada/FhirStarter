using System;
using System.Configuration;
using Serilog;
using Serilog.Core;

namespace FhirStarter.Bonfire.Log
{
    public static class SetupSerilogLogging
    {
        private static string SerilogFilePath = nameof(SerilogFilePath);
        private static string SerilogSplunkUrl = nameof(SerilogSplunkUrl);
        private static string SerilogSplunkViaTcpOrUdp = nameof(SerilogSplunkViaTcpOrUdp);
        private static string SplunkPort = nameof(SplunkPort);
        private static string SplunkHost = nameof(SplunkHost);

        private static string SplunkEventCollectorToken = nameof(SplunkEventCollectorToken);

        private static readonly string DefaultArgumentException = "The AppKey " + SerilogFilePath + " or " + SerilogSplunkUrl +
                                                        " must contain a valid value. Both cannot be empty";

        private static string _filePath;
        private static string _splunkUrl;
        private static string _splunkTcpOrUdp;
        private static string _splunkCollectorToken;
        private static int _splunkPort;
        private static string _splunkHost;

        public static bool IsFileLoggingEnabled()
        {
            return !string.IsNullOrEmpty(_filePath);
        }
        
        public static bool IsSplunkViaUrlEnabled()
        {
            throw new NotImplementedException();
            //return !string.IsNullOrEmpty(_splunkUrl);
        }

        public static bool IsSplunkViaTcpEnabled()
        {
            throw new NotImplementedException();
           // return !string.IsNullOrEmpty(_splunkTcpOrUdp) && _splunkTcpOrUdp.ToLower().Equals("tcp");
        }

        public static bool IsSplunkViaUdpEnabled()
        {
            throw new NotImplementedException();
            //return !string.IsNullOrEmpty(_splunkTcpOrUdp) && _splunkTcpOrUdp.ToLower().Equals("udp");
        }

        public static bool IsSplunkEnabled()
        {
            return IsFileLoggingEnabled();
        }


        private static void SetupConfig()
        {
            _filePath = ConfigurationManager.AppSettings[SerilogFilePath];

            _splunkUrl = ConfigurationManager.AppSettings[SerilogSplunkUrl];
            _splunkCollectorToken = ConfigurationManager.AppSettings[SplunkEventCollectorToken];


            _splunkTcpOrUdp = ConfigurationManager.AppSettings[SerilogSplunkViaTcpOrUdp];

            if (IsSplunkViaTcpEnabled() || IsSplunkViaUdpEnabled())
            {
                GetTcpOrUdpSettings();
            }

            if (string.IsNullOrEmpty(_filePath) && string.IsNullOrEmpty(_splunkUrl) && string.IsNullOrEmpty(_splunkTcpOrUdp))
            {
                throw new ArgumentException(DefaultArgumentException);
            }
        }

        private static void GetTcpOrUdpSettings()
        {
            _splunkHost = ConfigurationManager.AppSettings[SplunkHost];

            int port;
            var setting = ConfigurationManager.AppSettings[SplunkPort];
            var succesfull = Int32.TryParse(setting, out port);
            if (succesfull)
            {
                _splunkPort = port;
            }
            else
            {
                throw new ArgumentException(SplunkPort +
                                            " setting is not set properly, need to be a number that can be parsed to int32. Is currently " + setting);
            }
        }


        /// <summary>
        /// Setup for Serilog
        /// At minimum, you need to add
        ///   // <add key="SerilogFilePath" value="C:\Log\Serilog" />
        /// to the web.config 
        /// </summary>
        /// <returns></returns>
        public static Logger GetLogger()
        {
            SetupConfig();

            if (IsFileLoggingEnabled())
            {
                return SetupFileLogging();
            }

            if (IsSplunkViaUrlEnabled())
            {
                return SetupSplunkUrlLogging();
            }

            if (IsSplunkViaTcpEnabled())
            {
                return SetupSplunkTcpLogging();
            }
            if (IsSplunkViaUdpEnabled())
            {
                return SetupSplunkUdpLogging();
            }

           // throw new ArgumentException(DefaultArgumentException);
            return null;
        }

        public static Logger SetupFileLogging()
        {
            return new LoggerConfiguration().MinimumLevel.Debug().WriteTo.RollingFile(_filePath).CreateLogger();
        }

        public static Logger SetupSplunkUrlLogging()
        {
            if (!string.IsNullOrEmpty(_splunkCollectorToken))
            {
                return new LoggerConfiguration().WriteTo.EventCollector(
                        _splunkUrl, _splunkCollectorToken)
                    .CreateLogger();
            }
            throw new ArgumentException("Missing the setting " + SplunkEventCollectorToken +
                                        " or value in the setting");
        }


        public static Logger SetupSplunkTcpLogging()
        {
            //var log = new LoggerConfiguration()
            //    .WriteTo.SplunkViaTcp(_splunkHost, _splunkPort).CreateLogger();
            //return log;
           throw new NotImplementedException("Not sure how to implement it yet since documentation for Serilog is lacking. I only find an obsolete method that might be removed soon");
        }

        public static Logger SetupSplunkUdpLogging()
        {
            //var log = new LoggerConfiguration()
            //    .WriteTo.SplunkViaUdp(_splunkHost, _splunkPort).CreateLogger();
            //return log;
            throw new NotImplementedException("Not sure how to implement it yet since documentation for Serilog is lacking. I only find an obsolete method that might be removed soon");
        }
    }
}
