using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace QA_Automation_Databases
{
    public class BaseTest
    {
        protected MySqlConnection _conn;
        protected MySqlQueries _query;

        [OneTimeSetUp]
        protected void DoBeforeAllTests()
        {
            DBMySqlUtils dBMySql = new DBMySqlUtils();

            _conn = dBMySql.GetDBConnection();
            _conn.Open();

            _query = new MySqlQueries();
        }

        [OneTimeTearDown]
        protected void DoAfterAllTests()
        {
            _conn.Close();
            _conn.Dispose();
        }
    }
}
