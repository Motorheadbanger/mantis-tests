using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectDeletionTests : TestBase
    {
        [Test]
        public void ProjectDeletionTest()
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
            applicationManager.ProjectManagementHelper.Delete(project);

            Assert.IsFalse(applicationManager.ProjectManagementHelper.Exists(project));
        }
    }
}
