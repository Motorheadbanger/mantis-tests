﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace mantis_tests
{
    public class ApplicationManager
    {
        public IWebDriver Driver { get; }
        public string BaseUrl { get; }
        public RegistrationHelper RegistrationHelper {get; set;}
        public FtpHelper FtpHelper { get; set; }
        public JamesHelper JamesHelper { get; set; }
        public MailHelper MailHelper { get; set; }

        private static readonly ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            Driver = new FirefoxDriver();
            BaseUrl = "http://localhost/addressbook";
            RegistrationHelper = new RegistrationHelper(this);
            FtpHelper = new FtpHelper(this);
            JamesHelper = new JamesHelper(this);
            MailHelper = new MailHelper(this);
        }

        public static ApplicationManager GetInstance()
        {
            if (!applicationManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Driver.Url = "http://localhost/mantisbt-2.24.2/login_page.php";
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
