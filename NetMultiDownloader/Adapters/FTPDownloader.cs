using CoreFtp;
using CoreFtp.Enum;
using NetMultiDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetMultiDownloader.Adapters
{
    internal class FTPDownloader : IDownloader
    {
        public async Task<Result> DownloadAsync(Uri Uri, int retryNo, string path)
        {
            Result rs = new Result(Uri);

            var userInfo = Uri.UserInfo.Split(":");
            var userName = userInfo[0];
            var pass = userInfo.Length > 1 ? userInfo[1] : "";
            var resultFileName = FileNameHelper.GetDeDupFileName(Uri);
            for (var retried = 0; retried < retryNo; retried++)
            {
                try
                {
                    using (var ftpClient = new FtpClient(new FtpClientConfiguration
                    {
                        Host = Uri.Host,
                        Username = userName == "" ? null : userName,
                        Password = pass == "" ? null : pass,
                        Port = Uri.Port,
                        EncryptionType = FtpEncryption.Implicit,
                        IgnoreCertificateErrors = true
                    }))
                    {
                        var filename = System.IO.Path.GetFileNameWithoutExtension(Uri.LocalPath);
                        var extension = System.IO.Path.GetExtension(Uri.LocalPath);
                        
                        var tempFile = new FileInfo($"{path}{resultFileName}");
                        await ftpClient.LoginAsync();
                        using (var ftpReadStream = await ftpClient.OpenFileReadStreamAsync(Uri.LocalPath))
                        {
                            using (var fileWriteStream = tempFile.OpenWrite())
                            {
                                await ftpReadStream.CopyToAsync(fileWriteStream);
                            }
                        }
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
                
            return rs;
        }
    }
}
