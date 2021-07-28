using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace QA_Automation_Databases
{
    public static class DBMySqlUtils
    {
        private static string _nameOfExceptionLog = "Exceptions";
        private static string _nameOfMinWorkingTimeLog = "MinWorkingTime";
        private static string _nameOfUniqueProjectTestsLog = "UniqueProjectTests";
        private static string _nameOfTestsAfter7NovemberLog = "TestsAfter7November";
        private static string _nameOfNumberOfTestsForBrowserLog = "NumberOfTestsForBrowser";

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

        public static void ExecuteMinWorkingTimeQuery(string sqlQuery)
        {
            MySqlCommand cmd = GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(_nameOfMinWorkingTimeLog);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int projectIndex = reader.GetOrdinal("PROJECT");
                    int testIndex = reader.GetOrdinal("TEST");
                    int minWorkingTimeIndex = reader.GetOrdinal("MIN_WORKING_TIME");

                    log.LogWrite(String.Format($"| {{{projectIndex}, {MySqlQueries.GetProjectNameSymbolsCount}}} |" +
                        $" {{{testIndex}, {MySqlQueries.GetTestNameSymbolsCount}}} |" +
                        $" {{{minWorkingTimeIndex}, {MySqlQueries.GetMinWorkingTimeSymbolsCount}}} |",
                        reader.GetName(projectIndex), reader.GetName(testIndex), reader.GetName(minWorkingTimeIndex)));
                    while (reader.Read())
                    {
                        string project = reader.GetString(projectIndex);
                        string test = reader.GetString(testIndex);
                        string minWorkingTime = reader.GetString(minWorkingTimeIndex);

                        log.LogWrite(String.Format($"| {{{projectIndex}, {MySqlQueries.GetProjectNameSymbolsCount}}} |" +
                            $" {{{testIndex}, {MySqlQueries.GetTestNameSymbolsCount}}} |" +
                            $" {{{minWorkingTimeIndex}, {MySqlQueries.GetMinWorkingTimeSymbolsCount}}} |",
                            project, test, minWorkingTime));
                    }
                }
            }
        }

        public static void ExecuteUniqueProjectTestsQuery(string sqlQuery)
        {
            MySqlCommand cmd = GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(_nameOfUniqueProjectTestsLog);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int projectIndex = reader.GetOrdinal("PROJECT");
                    int testCountIndex = reader.GetOrdinal("TESTS_COUNT");

                    log.LogWrite(String.Format($"| {{{projectIndex}, {MySqlQueries.GetProjectNameSymbolsCount}}} |" +
                        $" {{{testCountIndex}, {MySqlQueries.GetTestCountSymbolsCount}}} |",
                        reader.GetName(projectIndex), reader.GetName(testCountIndex)));

                    while (reader.Read())
                    {
                        string project = reader.GetString(projectIndex);
                        string testCount = reader.GetString(testCountIndex);

                        log.LogWrite(String.Format($"| {{{projectIndex}, {MySqlQueries.GetProjectNameSymbolsCount}}}" +
                            $" | {{{testCountIndex}, {MySqlQueries.GetTestCountSymbolsCount}}} |",
                            project, testCount));
                    }
                }
            }
        }

        public static void ExecuteTestsAfter7NovemberQuery(string sqlQuery)
        {
            MySqlCommand cmd = GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(_nameOfTestsAfter7NovemberLog);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int projectIndex = reader.GetOrdinal("PROJECT");
                    int testIndex = reader.GetOrdinal("TEST");
                    int dateIndex = reader.GetOrdinal("DATE");

                    log.LogWrite(String.Format($"| {{{projectIndex}, {MySqlQueries.GetProjectNameSymbolsCount}}} |" +
                        $" {{{testIndex}, {MySqlQueries.GetTestNameSymbolsCount}}} |" +
                        $" {{{dateIndex}, {MySqlQueries.GetDateSymbolsCount}}} |",
                        reader.GetName(projectIndex), reader.GetName(testIndex), reader.GetName(dateIndex)));

                    while (reader.Read())
                    {
                        string project = reader.GetString(projectIndex);
                        string test = reader.GetString(testIndex);
                        string date = reader.GetString(dateIndex);

                        log.LogWrite(String.Format($"| {{{projectIndex}, {MySqlQueries.GetProjectNameSymbolsCount}}} |" +
                            $" {{{testIndex}, {MySqlQueries.GetTestNameSymbolsCount}}} |" +
                            $" {{{dateIndex}, {MySqlQueries.GetDateSymbolsCount}}} |",
                            project, test, date));
                    }
                }
            }
        }

        public static void ExecuteNumberOfTestsForBrowserQuery(string sqlQuery)
        {
            MySqlCommand cmd = GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(_nameOfNumberOfTestsForBrowserLog);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int browserIndex = reader.GetOrdinal("BROWSER");
                    int testCountIndex = reader.GetOrdinal("TESTS_COUNT");

                    log.LogWrite(String.Format($"| {{{browserIndex}, {MySqlQueries.GetBrowserSymbolsCount}}} |" +
                        $" {{{testCountIndex}, {MySqlQueries.GetTestCountSymbolsCount}}} |",
                        reader.GetName(browserIndex), reader.GetName(testCountIndex)));

                    while (reader.Read())
                    {
                        string browser = reader.GetString(browserIndex);
                        string testCount = reader.GetString(testCountIndex);

                        log.LogWrite(String.Format($"| {{{browserIndex}, {MySqlQueries.GetBrowserSymbolsCount}}} |" +
                            $" {{{testCountIndex}, {MySqlQueries.GetTestCountSymbolsCount}}} |",
                            browser, testCount));
                    }
                }
            }
        }
    }
}