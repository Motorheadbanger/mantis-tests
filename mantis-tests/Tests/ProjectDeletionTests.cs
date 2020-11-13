using NUnit.Framework;
using System.Collections.Generic;

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

            if (!applicationManager.ProjectManagementHelper.Exists(project))
                applicationManager.ProjectManagementHelper.Create(project);

            List<ProjectData> initialProjectList = applicationManager.ProjectManagementHelper.GetProjectList();

            applicationManager.ProjectManagementHelper.Delete(project);

            List<ProjectData> modifiedProjectList = applicationManager.ProjectManagementHelper.GetProjectList();

            initialProjectList.Remove(project);
            initialProjectList.Sort();
            modifiedProjectList.Sort();

            Assert.AreEqual(initialProjectList, modifiedProjectList);
        }
    }
}
