using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public void Create(ProjectData project)
        {
            driver.FindElement(By.XPath("//form[@action='manage_proj_create_page.php']")).Click();
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            driver.FindElement(By.CssSelector("input.btn")).Click();
            Thread.Sleep(3000);
        }

        public void Delete(ProjectData project)
        {
            List<ProjectData> list = GetProjectList();

            foreach (ProjectData item in list)
                if (item.Name == project.Name)
                    project.Id = item.Id;

            driver.FindElement(By.XPath("//a[@href='manage_proj_edit_page.php?project_id=" + project.Id + "']")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            Thread.Sleep(500);
        }

        public bool Exists(ProjectData project)
        {
            List<ProjectData> projectList = GetProjectList();

            return projectList.Contains(project);
        }

        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> list = new List<ProjectData>();
            IList<IWebElement> elements = driver.FindElements(By.XPath("//a[contains(@href, 'manage_proj_edit_page.php')]"));

            foreach (IWebElement element in elements)
            {
                list.Add(new ProjectData()
                {
                    Name = element.Text,
                    Id = element.GetAttribute("href").Split('=')[1]
                });
            }

            return list;
        }
    }
}