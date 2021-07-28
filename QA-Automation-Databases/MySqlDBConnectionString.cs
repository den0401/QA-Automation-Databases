using QA_Automation_Databases.Utils;

namespace QA_Automation_Databases
{
    public class MySqlDBConnectionString
    {
        private static string _dbDataSource;
        private static string _dbName;
        private static string _dbPort;
        private static string _dbUserName;
        private static string _dbPassword;        

        public static string DBSource => _dbDataSource ?? (_dbDataSource =
            JsonReader.ReadConnectionStringConfigFromJson().Host);

        public static string DBName => _dbName ?? (_dbName =
            JsonReader.ReadConnectionStringConfigFromJson().Database);

        public static string DBPort => _dbPort ?? (_dbPort =
            JsonReader.ReadConnectionStringConfigFromJson().Port);

        public static string DBUser => _dbUserName ?? (_dbUserName =
            JsonReader.ReadConnectionStringConfigFromJson().Username);

        public static string DBPassword => _dbPassword ?? (_dbPassword =
            JsonReader.ReadConnectionStringConfigFromJson().Password);
    }
}