using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected string baseUrl;

        public IWebDriver Driver { get; }
        public RegistrationHelper RegistrationHelper {get; set;}
        public FtpHelper FtpHelper { get; set; }
        public JamesHelper JamesHelper { get; set; }
        public MailHelper MailHelper { get; set; }
        public LoginHelper LoginHelper { get; set; }
        public ManagementMenuHelper ManagementMenuHelper { get; set; }
        public ProjectManagementHelper ProjectManagementHelper { get; set; }
        public MainMenuHelper MainMenuHelper { get; set; }
        public AdminHelper AdminHelper { get; set; }
        public ApiHelper ApiHelper { get; set; }

        private static readonly ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            Driver = new FirefoxDriver();
            baseUrl = "http://localhost/mantisbt-2.24.2";
            RegistrationHelper = new RegistrationHelper(this);
            FtpHelper = new FtpHelper(this);
            JamesHelper = new JamesHelper(this);
            MailHelper = new MailHelper(this);
            LoginHelper = new LoginHelper(this);
            ManagementMenuHelper = new ManagementMenuHelper(this);
            ProjectManagementHelper = new ProjectManagementHelper(this);
            MainMenuHelper = new MainMenuHelper(this);
            AdminHelper = new AdminHelper(this, baseUrl);
            ApiHelper = new ApiHelper(this);
        }

        public static ApplicationManager GetInstance()
        {
            if (!applicationManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Driver.Url = newInstance.baseUrl + "/login_page.php";
                applicationManager.Value = newInstance;
            }

            return applicationManager.Value;
        }

        ~ApplicationManager()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
