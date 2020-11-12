

using MinimalisticTelnet;
using System;

namespace mantis_tests
{
    public class JamesHelper : HelperBase
    {
        public JamesHelper(ApplicationManager applicationManager) : base(applicationManager)
        {

        }

        public void Add(AccountData account)
        {
            if (Verify(account))
                return;

            TelnetConnection telnetConnection = JamesLogin();

            telnetConnection.WriteLine("adduser " + account.Name + " " + account.Password);
        }

        public void Delete(AccountData account)
        {
            if (!Verify(account))
                return;

            TelnetConnection telnetConnection = JamesLogin();

            telnetConnection.WriteLine("deluser " + account.Name);
        }

        public bool Verify(AccountData account)
        {
            TelnetConnection telnetConnection = JamesLogin();

            telnetConnection.WriteLine("verify " + account.Name);

            string answer = telnetConnection.Read();
            Console.Out.WriteLine(answer);

            return answer.Contains("exists");
        }

        private TelnetConnection JamesLogin()
        {
            TelnetConnection telnetConnection = new TelnetConnection("localhost", 4555);

            Console.Out.WriteLine(telnetConnection.Read());
            telnetConnection.WriteLine("root");
            Console.Out.WriteLine(telnetConnection.Read());
            telnetConnection.WriteLine("root");
            Console.Out.WriteLine(telnetConnection.Read());

            return telnetConnection;
        }
    }
}
