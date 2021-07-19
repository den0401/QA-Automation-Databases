using MySql.Data.MySqlClient;
using QA_Automation_Databases.Utils;

namespace QA_Automation_Databases
{
    public class DBMySqlUtils
    {
        private readonly string _host;
        private readonly string _database;
        private readonly string _port;
        private readonly string _username;
        private readonly string _password;

        public DBMySqlUtils(
            string host = null,
            string database = null,
            string port = null,
            string username = null,
            string password = null)
        {
            _host = host ?? JsonReader.ReadConnectionStringConfigFromJson().Host;
            _database = database ?? JsonReader.ReadConnectionStringConfigFromJson().Database;
            _port = port ?? JsonReader.ReadConnectionStringConfigFromJson().Port;
            _username = username ?? JsonReader.ReadConnectionStringConfigFromJson().Username;
            _password = password ?? JsonReader.ReadConnectionStringConfigFromJson().Password;
        }

        public MySqlConnection GetDBConnection()
        {
            string connString = $"Server={_host};Database={_database};Port={_port};" +
                $"Username={_username};Password={_password};";

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}