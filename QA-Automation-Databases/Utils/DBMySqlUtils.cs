using MySql.Data.MySqlClient;
using System;

namespace QA_Automation_Databases
{
    public static class DBMySqlUtils
    {
        private static readonly string _nameOfExceptionLog = "Exceptions";

        private static string DefaultConnectionString => string.Format(
            $"Server={MySqlDBConnectionString.DBSource};Database={MySqlDBConnectionString.DBName};" +
            $"Port={MySqlDBConnectionString.DBPort};Username={MySqlDBConnectionString.DBUser};" +
            $"Password={MySqlDBConnectionString.DBPassword};");

        public static string GetMySqlConnectionString(string connectionString) =>
            connectionString.Equals(string.Empty) ? DefaultConnectionString : connectionString;

        public static MySqlConnection GetConnection(string connectionString = "")
        {
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(GetMySqlConnectionString(connectionString));
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                Logger log = new Logger(_nameOfExceptionLog);

                try
                {
                    connection?.Dispose();
                }
                catch (Exception ex1)
                {
                    log.LogWrite($"Connection can't be closed!\nError: {ex1.Message}");
                }

                log.LogWrite($"Connection can't be opened!\nError: {ex.Message}");
            }

            return null;
        }        
    }
}