using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (Exists(project))
                Delete(project);

            driver.FindElement(By.XPath("//form[@action='manage_proj_create_page.php']")).Click();
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            driver.FindElement(By.CssSelector("input.btn")).Click();
            Thread.Sleep(3000);
        }

        public void Delete(ProjectData project)
        {
            if (!Exists(project))
                Create(project);

            Dictionary<string, string> projectList = GetProjectList();

            driver.FindElement(By.XPath("//a[@href='manage_proj_edit_page.php?project_id=" + projectList[project.Name] + "']")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            Thread.Sleep(500);
        }

        public bool Exists(ProjectData project)
        {
            Dictionary<string, string> projectList = GetProjectList();

            return projectList.ContainsKey(project.Name);
        }

        private Dictionary<string, string> GetProjectList()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("//a[contains(@href, 'manage_proj_edit_page.php')]"));

            foreach (IWebElement element in elements)
            {
                list.Add(element.Text, element.GetAttribute("href").Split('=')[1]);
            }

            return list;
        }
    }
}