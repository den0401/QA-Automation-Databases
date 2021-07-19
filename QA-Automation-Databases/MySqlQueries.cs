using MySql.Data.MySqlClient;
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
            "WHERE start_time >= '2015-11-07 00:00:00' " +
            "GROUP BY project.name, test.name " +
            "ORDER BY project.name, test.name";

        private readonly string _sqlQueryNumberOfTestsForBrowser =
            "SELECT browser AS BROWSER, COUNT(browser) AS TESTS_COUNT " +
            "FROM union_reporting.test " +
            "WHERE browser = 'chrome' " +
            "UNION " +
            "SELECT browser AS BROWSER, COUNT(browser) AS TESTS_COUNT " +
            "FROM union_reporting.test " +
            "WHERE browser = 'firefox' ";

        private const int ProjectNameSymbolsCount = -11;
        private const int TestNameSymbolsCount = -146;
        private const int MinWorkingTimeSymbolsCount = -16;
        private const int TestCountSymbolsCount = -11;
        private const int DateSymbolsCount = -22;
        private const int BrowserSymbolsCount = -7;

        private void ExecuteQuery(MySqlConnection conn, string sqlQuery, string switchToQueryOutput)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = sqlQuery;

            Logger log = new Logger();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (switchToQueryOutput == "MinWorkingTime")
                    {
                        int projectIndex = reader.GetOrdinal("PROJECT");
                        int testIndex = reader.GetOrdinal("TEST");
                        int minWorkingTimeIndex = reader.GetOrdinal("MIN_WORKING_TIME");

                        log.LogWrite(String.Format($"| {{{projectIndex}, {ProjectNameSymbolsCount}}} | {{{testIndex}, {TestNameSymbolsCount}}} | {{{minWorkingTimeIndex}, {MinWorkingTimeSymbolsCount}}} |",
                        reader.GetName(projectIndex), reader.GetName(testIndex), reader.GetName(minWorkingTimeIndex)));

                        while (reader.Read())
                        {
                            string project = reader.GetString(projectIndex);
                            string test = reader.GetString(testIndex);
                            string minWorkingTime = reader.GetString(minWorkingTimeIndex);

                            log.LogWrite(String.Format($"| {{{projectIndex}, {ProjectNameSymbolsCount}}} | {{{testIndex}, {TestNameSymbolsCount}}} | {{{minWorkingTimeIndex}, {MinWorkingTimeSymbolsCount}}} |",
                                project, test, minWorkingTime));
                        }
                    }
                    else if (switchToQueryOutput == "UniqueProjectTests")
                    {
                        int projectIndex = reader.GetOrdinal("PROJECT");
                        int testCountIndex = reader.GetOrdinal("TESTS_COUNT");

                        log.LogWrite(String.Format($"| {{{projectIndex}, {ProjectNameSymbolsCount}}} | {{{testCountIndex}, {TestCountSymbolsCount}}} |",
                        reader.GetName(projectIndex), reader.GetName(testCountIndex)));

                        while (reader.Read())
                        {
                            string project = reader.GetString(projectIndex);
                            string testCount = reader.GetString(testCountIndex);

                            log.LogWrite(String.Format($"| {{{projectIndex}, {ProjectNameSymbolsCount}}} | {{{testCountIndex}, {TestCountSymbolsCount}}} |",
                                project, testCount));
                        }
                    }
                    else if (switchToQueryOutput == "TestsAfter7November")
                    {
                        int projectIndex = reader.GetOrdinal("PROJECT");
                        int testIndex = reader.GetOrdinal("TEST");
                        int dateIndex = reader.GetOrdinal("DATE");

                        log.LogWrite(String.Format($"| {{{projectIndex}, {ProjectNameSymbolsCount}}} | {{{testIndex}, {TestNameSymbolsCount}}} | {{{dateIndex}, {DateSymbolsCount}}} |",
                        reader.GetName(projectIndex), reader.GetName(testIndex), reader.GetName(dateIndex)));

                        while (reader.Read())
                        {
                            string project = reader.GetString(projectIndex);
                            string test = reader.GetString(testIndex);
                            string date = reader.GetString(dateIndex);

                            log.LogWrite(String.Format($"| {{{projectIndex}, {ProjectNameSymbolsCount}}} | {{{testIndex}, {TestNameSymbolsCount}}} | {{{dateIndex}, {DateSymbolsCount}}} |",
                                project, test, date));
                        }
                    }
                    else if (switchToQueryOutput == "NumberOfTestsForBrowser")
                    {
                        int browserIndex = reader.GetOrdinal("BROWSER");
                        int testCountIndex = reader.GetOrdinal("TESTS_COUNT");

                        log.LogWrite(String.Format($"| {{{browserIndex}, {BrowserSymbolsCount}}} | {{{testCountIndex}, {TestCountSymbolsCount}}} |",
                        reader.GetName(browserIndex), reader.GetName(testCountIndex)));

                        while (reader.Read())
                        {
                            string browser = reader.GetString(browserIndex);
                            string testCount = reader.GetString(testCountIndex);

                            log.LogWrite(String.Format($"| {{{browserIndex}, {BrowserSymbolsCount}}} | {{{testCountIndex}, {TestCountSymbolsCount}}} |",
                                browser, testCount));
                        }
                    }
                    else throw new ArgumentException();

                    log.LogWrite(string.Empty);
                }
            }
        }

        public void MinWorkingTime(MySqlConnection conn) =>
            ExecuteQuery(conn, _sqlQueryMinWorkingTime, "MinWorkingTime");

        public void UniqueProjectTests(MySqlConnection conn) =>
            ExecuteQuery(conn, _sqlQueryUniqueProjectTests, "UniqueProjectTests");
        
        public void QueryTestsAfter7November(MySqlConnection conn) =>
            ExecuteQuery(conn, _sqlQueryTestsAfter7November, "TestsAfter7November");
        
        public void NumberOfTestsForBrowser(MySqlConnection conn) =>
            ExecuteQuery(conn, _sqlQueryNumberOfTestsForBrowser, "NumberOfTestsForBrowser");
    }
}