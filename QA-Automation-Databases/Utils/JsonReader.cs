using Microsoft.Extensions.Configuration;
using System;

namespace QA_Automation_Databases.Utils
{
    class JsonReader
    {
        public static ConnectionStringConfig ReadConnectionStringConfigFromJson()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(ConnectionStringConfig));
            var connectionStringConfig = section.Get<ConnectionStringConfig>();

            return connectionStringConfig;
        }
    }
}