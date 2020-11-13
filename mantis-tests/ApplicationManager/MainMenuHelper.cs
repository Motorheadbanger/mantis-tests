using OpenQA.Selenium;

namespace mantis_tests
{
    public class MainMenuHelper : HelperBase
    {
        public MainMenuHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public void SwitchToManageTab()
        {
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.24.2/manage_overview_page.php']")).Click();
        }
    }
}