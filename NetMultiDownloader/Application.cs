using NetMultiDownloader.Adapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetMultiDownloader
{
    public class Application
    {
        private IQueueProcessor processor;
        public Application(IQueueProcessor processor)
        {
            this.processor = processor;
        }
        public async Task Run()
        {
            StreamReader r = new StreamReader("config.json");
            string jsonString = r.ReadToEnd();
            models.Config config = JsonConvert.DeserializeObject<models.Config>(jsonString);
            await this.processor.QueueProcessAsync(config);
        }
    }
}
