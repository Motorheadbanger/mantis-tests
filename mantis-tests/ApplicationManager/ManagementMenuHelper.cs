using OpenQA.Selenium;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        public ManagementMenuHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public void SwitchToManageProjectsTab()
        {
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.24.2/manage_proj_page.php']")).Click();
        }
    }
}