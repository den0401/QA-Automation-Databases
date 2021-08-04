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
            Assert.IsFalse(_query.IsMinWorkingTimeFileEmptyOrNotExist());
        }

        [Test]
        public void UniqueProjectTestsTest()
        {
            _query.UniqueProjectTests();
            Assert.IsFalse(_query.IsUniqueProjectTestsFileEmptyOrNotExist());
        }        

        [Test]
        public void QueryTestsAfter7NovemberTest()
        {
            _query.TestsAfter7November();
            Assert.IsFalse(_query.IsTestsAfter7NovemberFileEmptyOrNotExist());
        }

        [Test]
        public void NumberOfTestsForBrowserTest()
        {
            _query.NumberOfTestsForBrowser();
            Assert.IsFalse(_query.IsNumberOfTestsForBrowserFileEmptyOrNotExist());
        }
    }
}