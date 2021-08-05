using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace QA_Automation_Databases
{
    public class BaseTest
    {
        protected MySqlQueries _query;

        [OneTimeSetUp]
        protected void DoBeforeAllTests()
        {
            _query = new MySqlQueries();
        }
    }
}
