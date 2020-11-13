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

            applicationManager.LoginHelper.Login(admin);
            applicationManager.MainMenuHelper.SwitchToManageTab();
            applicationManager.ManagementMenuHelper.SwitchToManageProjectsTab();

            if (applicationManager.ApiHelper.GetProjectList(admin).Count == 0)
                applicationManager.ApiHelper.Create("Emergency project", admin);

            List<ProjectData> initialProjectList = applicationManager.ProjectManagementHelper.GetProjectList(admin);
            ProjectData toBeRemoved = initialProjectList[0];

            applicationManager.ProjectManagementHelper.Delete(toBeRemoved, admin);

            List<ProjectData> modifiedProjectList = applicationManager.ProjectManagementHelper.GetProjectList(admin);

            initialProjectList.Remove(toBeRemoved);
            initialProjectList.Sort();
            modifiedProjectList.Sort();

            Assert.AreEqual(initialProjectList, modifiedProjectList);
        }
    }
}
