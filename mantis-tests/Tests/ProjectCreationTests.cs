using NUnit.Framework;
using System.Collections.Generic;

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

            if (applicationManager.ProjectManagementHelper.Exists(project, admin))
                applicationManager.ProjectManagementHelper.Delete(project, admin);

            List<ProjectData> initialProjectList = applicationManager.ProjectManagementHelper.GetProjectList(admin);

            applicationManager.ProjectManagementHelper.Create(project);

            List<ProjectData> modifiedProjectList = applicationManager.ProjectManagementHelper.GetProjectList(admin);

            initialProjectList.Add(project);
            initialProjectList.Sort();
            modifiedProjectList.Sort();

            Assert.AreEqual(initialProjectList, modifiedProjectList);
        }
    }
}
