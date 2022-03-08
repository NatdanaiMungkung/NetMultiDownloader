using NetMultiDownloader.Adapters;
using NetMultiDownloader.models;
using NetMultiDownloader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetMultiDownloader
{
    public class QueueProcessor : IQueueProcessor
    {
        private readonly IAdapterFactory adapterFactory;
        public QueueProcessor(IAdapterFactory adapterFactory)
        {
            this.adapterFactory = adapterFactory;
        }
        public async Task<Result[]> QueueProcessAsync(Config config)
        {
            var distinctUri = config.Uris.Distinct();
            var resultList = await Task.WhenAll(distinctUri.AsParallel().Select(async a =>
            {
                var uriObj = new Uri(a);
                var downloader = adapterFactory.GetDownloader(uriObj);
                var result = await downloader.DownloadAsync(uriObj, config.RetriesNo, config.Path);
                Console.WriteLine($"{a}-------------{(result.IsCompleted ? result.FileName : result.Error)}");
                return result;
            }));
            return resultList;
        }
    }
}
