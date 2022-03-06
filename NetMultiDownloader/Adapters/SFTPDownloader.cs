using NetMultiDownloader.Models;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetMultiDownloader.Adapters
{
    internal class SFTPDownloader : IDownloader
    {
        public Task<Result> DownloadAsync(Uri Uri, int retryNo, string path)
        {
            Result rs = new Result(Uri);
            var filename = System.IO.Path.GetFileNameWithoutExtension(Uri.LocalPath);
            var extension = System.IO.Path.GetExtension(Uri.LocalPath);
            var resultFileName = FileNameHelper.GetDeDupFileName(Uri);
            var tempFile = new FileInfo($"{path}{resultFileName}");
            var userInfo = Uri.UserInfo.Split(":");
            var userName = userInfo[0];
            var pass = userInfo.Length > 1 ? userInfo[1] : "";
            
            for (var retried = 0; retried < retryNo; retried++)
            {
                try
                {
                    using (var sftpClient = new SftpClient(Uri.Host, userName, pass))
                    using (var fs = new FileStream(tempFile.ToString(), FileMode.OpenOrCreate))
                    {
                        sftpClient.Connect();

                        sftpClient.DownloadFile(
                            Uri.LocalPath,
                            fs
                            /*downloaded =>
                            {
                                Console.WriteLine($"Downloaded {(double)downloaded / fs.Length * 100}% of the file.");
                            }*/);

                        sftpClient.Disconnect();
                    }
                    retried = retryNo;
                    rs.IsCompleted = true;
                    rs.Error = null;
                    rs.FileName = resultFileName;
                }
                catch (Exception ex)
                {
                    rs.IsCompleted = false;
                    rs.Error = ex.Message;
                }
            }
            
            return Task.FromResult(rs);
        }
    }
}
