using NUnit.Framework;
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
                Name = "testuser9",
                Password = "password",
                Email = "testuser9@localhost.localdomain"
            };

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
