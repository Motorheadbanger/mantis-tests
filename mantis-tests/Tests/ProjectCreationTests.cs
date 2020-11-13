using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        [Test]
        public void CreateProjectTest()
        {
            AccountData admin = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Name = "Test Project"
            };

            applicationManager.LoginHelper.Login(admin);
            applicationManager.MainMenuHelper.SwitchToManageTab();
            applicationManager.ManagementMenuHelper.SwitchToManageProjectsTab();
            applicationManager.ProjectManagementHelper.Create(project);

            Assert.IsTrue(applicationManager.ProjectManagementHelper.Exists(project));
        }
    }
}
