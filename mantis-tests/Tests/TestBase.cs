using NUnit.Framework;

namespace mantis_tests
{
    public class TestBase
    {
        protected ApplicationManager applicationManager;

        [TestFixtureSetUp]
        public void SetupApplicationManager()
        {
            applicationManager = ApplicationManager.GetInstance();
        }
    }
}
