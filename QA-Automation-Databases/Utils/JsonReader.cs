using Microsoft.Extensions.Configuration;
using System;

namespace QA_Automation_Databases.Utils
{
    public static class JsonReader
    {
        public static ConnectionStringConfig ReadConnectionStringConfigFromJson()
        {
            var config = GetConfig("appsettingsconfig.json");

            var section = config.GetSection(nameof(ConnectionStringConfig));
            var connectionStringConfig = section.Get<ConnectionStringConfig>();

            return connectionStringConfig;
        }

        public static MySqlQueryDataConfig ReadMySqlQueryDataConfigFromJson()
        {
            var config = GetConfig("testdata.json");

            var section = config.GetSection(nameof(MySqlQueryDataConfig));
            var mySqlQueryDataConfig = section.Get<MySqlQueryDataConfig>();

            return mySqlQueryDataConfig;
        }

        private static IConfiguration GetConfig(string jsonFileName)
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(jsonFileName).Build();
        }
    }
}