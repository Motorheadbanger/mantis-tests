using OpenQA.Selenium;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public void Login(AccountData account)
        {
            driver.FindElement(By.Id("username")).SendKeys(account.Name);
            driver.FindElement(By.CssSelector(".width-40")).Click();
            driver.FindElement(By.Id("password")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector(".width-40")).Click();
        }
    }
}