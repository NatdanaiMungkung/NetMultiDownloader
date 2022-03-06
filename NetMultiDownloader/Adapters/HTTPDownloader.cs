using Downloader;
using NetMultiDownloader.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetMultiDownloader.Adapters
{
    internal class HTTPDownloader : IDownloader
    {
        public async Task<Result> DownloadAsync(Uri uri, int retryNo, string path)
        {
            Result rs = new Result(uri);
            var downloadOpt = new DownloadConfiguration()
            {
                ChunkCount = 8, // file parts to download, default value is 1
                MaxTryAgainOnFailover = retryNo, // the maximum number of times to fail
                ParallelDownload = true, // download parts of file as parallel or not. Default value is false
            };
            var downloader = new DownloadService(downloadOpt);
            DirectoryInfo savePath = new DirectoryInfo(path);
            var resultFileName = FileNameHelper.GetDeDupFileName(uri);
            try
            {
                var filename = System.IO.Path.GetFileNameWithoutExtension(uri.LocalPath);
                var extension = System.IO.Path.GetExtension(uri.LocalPath);
                var tempFile = new FileInfo($"{path}{resultFileName}");
                await downloader.DownloadFileTaskAsync(uri.ToString(), tempFile.ToString());
                rs.FileName = resultFileName;
            }
            catch (Exception ex)
            {
                rs.IsCompleted = false;
                rs.Error = ex.Message;
            }
            return rs;
        }
    }
}
