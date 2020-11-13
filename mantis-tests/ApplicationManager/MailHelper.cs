using OpaqueMail;
using System.Threading;

namespace mantis_tests
{
    public class MailHelper : HelperBase
    {
        public MailHelper(ApplicationManager applicationManager) : base (applicationManager)
        {
        }

        public string GetMail(AccountData account)
        {
            Pop3Client pop3Client = new Pop3Client("localhost", 110, account.Name, account.Password, false);

            pop3Client.Connect();
            pop3Client.Authenticate();

            for (int i = 0; i < 15; i++)
            {
                pop3Client = new Pop3Client("localhost", 110, account.Name, account.Password, false);

                pop3Client.Connect();
                pop3Client.Authenticate();

                if (pop3Client.GetMessageCount() > 0)
                {
                    MailMessage message = pop3Client.GetMessage(1);
                    string messageBody = message.Body;

                    pop3Client.DeleteMessage(1);
                    pop3Client.LogOut();

                    return messageBody;
                }

                else
                    Thread.Sleep(1000);
            }

            return null;
        }
    }
}
