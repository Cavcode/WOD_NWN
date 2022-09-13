using System;
using WOD.Game.Server.Enumeration;

namespace WOD.Game.Server
{
    public class ApplicationSettings
    {
        public string LogDirectory { get; }
        public string RedisIPAddress { get; }
        public ServerEnvironmentType ServerEnvironment { get; }

        private static ApplicationSettings _settings;
        public static ApplicationSettings Get()
        {
            if (_settings == null)
                _settings = new ApplicationSettings();

            return _settings;
        }

        private ApplicationSettings()
        {
            LogDirectory = Environment.GetEnvironmentVariable("WOD_APP_LOG_DIRECTORY");
            RedisIPAddress = Environment.GetEnvironmentVariable("NWNX_REDIS_HOST");

            var environment = Environment.GetEnvironmentVariable("WOD_ENVIRONMENT");
            if (!string.IsNullOrWhiteSpace(environment) && (environment == "prod" || environment == "production"))
            {
                ServerEnvironment = ServerEnvironmentType.Production;
            }
            else
            {
                ServerEnvironment = ServerEnvironmentType.Development;
            }
        }

    }
}