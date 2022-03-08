using NetMultiDownloader.Models;
using System;
using System.Threading.Tasks;

namespace NetMultiDownloader.Adapters
{
    public interface IDownloader
    {
        public Task<Result> DownloadAsync(Uri Uri, int retryNo, string path);
    }
}
