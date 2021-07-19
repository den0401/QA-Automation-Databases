using NUnit.Framework;

namespace QA_Automation_Databases
{
    [TestFixture]
    public class Tests : BaseTest
    {
        [Test]
        public void MinWorkingTimeTest()
        {
            _query.MinWorkingTime(_conn);
        }

        [Test]
        public void UniqueProjectTestsTest()
        {
            _query.UniqueProjectTests(_conn);
        }

        [Test]
        public void QueryTestsAfter7NovemberTest()
        {
            _query.QueryTestsAfter7November(_conn);
        }

        [Test]
        public void NumberOfTestsForBrowserTest()
        {
            _query.NumberOfTestsForBrowser(_conn);
        }
    }
}