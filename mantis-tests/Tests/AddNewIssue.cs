using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {
        [Test]
        public void AddNewIssueTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Id = "19"
            };

            IssueData issue = new IssueData()
            {
                Summary = "summary",
                Description = "description",
                Category = "General"
            };

            applicationManager.ApiHelper.CreateNewIssue(account, project, issue);
        }
    }
}
