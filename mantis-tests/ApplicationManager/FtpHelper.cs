using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{
    public class FtpHelper : HelperBase
    {
        private FtpClient ftpClient;

        public FtpHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
            ftpClient = new FtpClient
            {
                Credentials = new System.Net.NetworkCredential("user", "password"),
                Host = "localhost"
            };
            ftpClient.Connect();
        }

        public void BackupFile(string path)
        {
            string backupPath = path + ".bak";
            if (ftpClient.FileExists(backupPath))
                return;
            ftpClient.Rename(path, backupPath);
        }

        public void RestoreBackup(string path)
        {
            string backupPath = path + ".bak";

            if (!ftpClient.FileExists(backupPath))
                return;

            if (ftpClient.FileExists(path))
                ftpClient.DeleteFile(path);

            ftpClient.Rename(backupPath, path);
        }

        public void Upload(string path, Stream stream)
        {
            if (ftpClient.FileExists(path))
                ftpClient.DeleteFile(path);

            using (Stream ftpStream = ftpClient.OpenWrite(path))
            {
                byte[] buffer = new byte[8 * 1024];
                int count = stream.Read(buffer, 0, buffer.Length);

                while (count > 0)
                {
                    ftpStream.Write(buffer, 0, count);

                    count = stream.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
