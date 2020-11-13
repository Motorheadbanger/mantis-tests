using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Match = System.Text.RegularExpressions.Match;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager applicationManager, string baseUrl) : base(applicationManager)
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAccountList()
        {
            IWebDriver driver = Login();
            List<AccountData> list = new List<AccountData>();
            driver.Url = baseUrl + "/manage_user_page.php";
            IList<IWebElement> elements = driver.FindElements(By.XPath("//a[contains(@href, 'manage_user_edit_page.php')]"));

            foreach(IWebElement element in elements)
            {
                string name = element.Text;
                string href = element.GetAttribute("href");
                Match match = Regex.Match(href, @"\d+$");
                string id = match.Value;

                list.Add(new AccountData()
                {
                    Name = name,
                    Id = id
                });
            }

            return list;
        }

        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = Login();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.XPath("//input[@value='Delete User']")).Click();
            driver.FindElement(By.CssSelector("input.btn")).Click();
            Thread.Sleep(3000);
        }

        private IWebDriver Login()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "/login_page.php";

            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector(".width-40")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector(".width-40")).Click();

            return driver;
        }
    }
}
