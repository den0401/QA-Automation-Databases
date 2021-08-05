using MySql.Data.MySqlClient;
using QA_Automation_Databases.Utils;
using System;
using System.Data.Common;

namespace QA_Automation_Databases
{
    public class MySqlQueries
    {
        private readonly string _sqlQueryMinWorkingTime =
            "SELECT project.name AS PROJECT, test.name AS TEST, min(end_time-start_time) AS MIN_WORKING_TIME " +
            "FROM union_reporting.test " +
            "JOIN union_reporting.project " +
            "ON union_reporting.test.project_id = union_reporting.project.id " +
            "GROUP BY test.name " +
            "ORDER BY project.name, test.name";

        private readonly string _sqlQueryUniqueProjectTests =
            "SELECT DISTINCT project.name AS PROJECT, COUNT(DISTINCT test.name) AS TESTS_COUNT " +
            "FROM union_reporting.test " +
            "JOIN union_reporting.project " +
            "ON union_reporting.test.project_id = union_reporting.project.id " +
            "GROUP BY project.name";

        private readonly string _sqlQueryTestsAfter7November =
            "SELECT project.name AS PROJECT, test.name AS TEST, start_time AS DATE " +
            "FROM union_reporting.test " +
            "JOIN union_reporting.project " +
            "ON union_reporting.test.project_id = union_reporting.project.id " +
            $"WHERE start_time >= '{JsonReader.ReadMySqlQueryDataConfigFromJson().StartTime}' " +
            "GROUP BY project.name, test.name " +
            "ORDER BY project.name, test.name";

        private readonly string _sqlQueryNumberOfTestsForBrowser =
            "SELECT browser AS BROWSER, COUNT(browser) AS TESTS_COUNT " +
            "FROM union_reporting.test " +
            $"WHERE browser = '{JsonReader.ReadMySqlQueryDataConfigFromJson().BrowserChrome}' " +
            "UNION " +
            "SELECT browser AS BROWSER, COUNT(browser) AS TESTS_COUNT " +
            "FROM union_reporting.test " +
            $"WHERE browser = '{JsonReader.ReadMySqlQueryDataConfigFromJson().BrowserFirefox}' ";

        private const int ProjectNameSymbolsCount = -11;
        private const int TestNameSymbolsCount = -146;
        private const int MinWorkingTimeSymbolsCount = -16;
        private const int TestCountSymbolsCount = -11;
        private const int DateSymbolsCount = -22;
        private const int BrowserSymbolsCount = -7;
        private const string NameOfMinWorkingTimeLog = "MinWorkingTime";
        private const string NameOfUniqueProjectTestsLog = "UniqueProjectTests";
        private const string NameOfTestsAfter7NovemberLog = "TestsAfter7November";
        private const string NameOfNumberOfTestsForBrowserLog = "NumberOfTestsForBrowser";

        public static int GetProjectNameSymbolsCount => ProjectNameSymbolsCount;
        public static int GetTestNameSymbolsCount => TestNameSymbolsCount;
        public static int GetMinWorkingTimeSymbolsCount => MinWorkingTimeSymbolsCount;
        public static int GetTestCountSymbolsCount => TestCountSymbolsCount;
        public static int GetDateSymbolsCount => DateSymbolsCount;
        public static int GetBrowserSymbolsCount => BrowserSymbolsCount;

        public void MinWorkingTime() =>
            ExecuteMinWorkingTimeQuery(_sqlQueryMinWorkingTime);

        public void UniqueProjectTests() =>
            ExecuteUniqueProjectTestsQuery(_sqlQueryUniqueProjectTests);

        public void TestsAfter7November() =>
            ExecuteTestsAfter7NovemberQuery(_sqlQueryTestsAfter7November);

        public void NumberOfTestsForBrowser() =>
            ExecuteNumberOfTestsForBrowserQuery(_sqlQueryNumberOfTestsForBrowser);        

        public bool IsMinWorkingTimeFileEmptyOrNotExist() =>
            Logger.IsLogFileEmptyOrNotExist(NameOfMinWorkingTimeLog);

        public bool IsUniqueProjectTestsFileEmptyOrNotExist() =>
            Logger.IsLogFileEmptyOrNotExist(NameOfUniqueProjectTestsLog);

        public bool IsTestsAfter7NovemberFileEmptyOrNotExist() =>
            Logger.IsLogFileEmptyOrNotExist(NameOfTestsAfter7NovemberLog);

        public bool IsNumberOfTestsForBrowserFileEmptyOrNotExist() =>
            Logger.IsLogFileEmptyOrNotExist(NameOfNumberOfTestsForBrowserLog);

        public static void ExecuteMinWorkingTimeQuery(string sqlQuery)
        {
            MySqlCommand cmd = DBMySqlUtils.GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(NameOfMinWorkingTimeLog);

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
            MySqlCommand cmd = DBMySqlUtils.GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(NameOfUniqueProjectTestsLog);

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
            MySqlCommand cmd = DBMySqlUtils.GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(NameOfTestsAfter7NovemberLog);

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
            MySqlCommand cmd = DBMySqlUtils.GetConnection().CreateCommand();
            cmd.CommandText = sqlQuery;

            Logger log = new Logger(NameOfNumberOfTestsForBrowserLog);

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