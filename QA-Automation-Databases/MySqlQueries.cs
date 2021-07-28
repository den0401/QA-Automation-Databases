using QA_Automation_Databases.Utils;

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

        public static int GetProjectNameSymbolsCount => ProjectNameSymbolsCount;
        public static int GetTestNameSymbolsCount => TestNameSymbolsCount;
        public static int GetMinWorkingTimeSymbolsCount => MinWorkingTimeSymbolsCount;
        public static int GetTestCountSymbolsCount => TestCountSymbolsCount;
        public static int GetDateSymbolsCount => DateSymbolsCount;
        public static int GetBrowserSymbolsCount => BrowserSymbolsCount;

        public void MinWorkingTime() =>
            DBMySqlUtils.ExecuteMinWorkingTimeQuery(_sqlQueryMinWorkingTime);

        public void UniqueProjectTests() =>
            DBMySqlUtils.ExecuteUniqueProjectTestsQuery(_sqlQueryUniqueProjectTests);

        public void TestsAfter7November() =>
            DBMySqlUtils.ExecuteTestsAfter7NovemberQuery(_sqlQueryTestsAfter7November);

        public void NumberOfTestsForBrowser() =>
            DBMySqlUtils.ExecuteNumberOfTestsForBrowserQuery(_sqlQueryNumberOfTestsForBrowser);
    }    
}