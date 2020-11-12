using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationPage();
            FillRegistrationForm(account);
            SubmitRegistration();
            string url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.CssSelector(".width-100")).Click();
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        private string GetConfirmationUrl(AccountData account)
        {
            string message = applicationManager.MailHelper.GetMail(account);

            return Regex.Match(message, @"http://\S*").Value;
        }

        private void OpenMainPage()
        {
            applicationManager.Driver.Url = "http://localhost/mantisbt-2.24.2/login_page.php";
        }

        private void OpenRegistrationPage()
        {
            driver.FindElement(By.CssSelector(".back-to-login-link")).Click();
        }

        private void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        private void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector(".width-40")).Click();
        }
    }
}
