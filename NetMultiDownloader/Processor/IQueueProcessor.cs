using NetMultiDownloader.models;
using NetMultiDownloader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetMultiDownloader
{
    public interface IQueueProcessor
    {
        public Task<Result[]> QueueProcessAsync(Config config);
    }
}
