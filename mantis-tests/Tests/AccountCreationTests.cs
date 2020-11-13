using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {

        [TestFixtureSetUp]
        public void SetupConfig()
        {
            applicationManager.FtpHelper.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                applicationManager.FtpHelper.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser1",
                Password = "password",
                Email = "testuser1@localhost.localdomain"
            };

            List<AccountData> accounts = applicationManager.AdminHelper.GetAccountList();
            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);

            if (existingAccount != null)
                applicationManager.AdminHelper.DeleteAccount(existingAccount);

            applicationManager.JamesHelper.Delete(account);
            applicationManager.JamesHelper.Add(account);

            applicationManager.RegistrationHelper.Register(account);
        }

        [TestFixtureTearDown]
        public void RestoreConfig()
        {
            applicationManager.FtpHelper.RestoreBackup("");
        }
    }
}
