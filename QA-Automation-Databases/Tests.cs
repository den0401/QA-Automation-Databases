using NUnit.Framework;

namespace QA_Automation_Databases
{
    [TestFixture]
    public class Tests : BaseTest
    {
        [Test]
        public void MinWorkingTimeTest()
        {
            _query.MinWorkingTime();
        }

        [Test]
        public void UniqueProjectTestsTest()
        {
            _query.UniqueProjectTests();
        }        

        [Test]
        public void QueryTestsAfter7NovemberTest()
        {
            _query.TestsAfter7November();
        }

        [Test]
        public void NumberOfTestsForBrowserTest()
        {
            _query.NumberOfTestsForBrowser();
        }
    }
}