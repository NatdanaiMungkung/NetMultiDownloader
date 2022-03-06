using NetMultiDownloader.Adapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetMultiDownloader
{
    internal class Application
    {
        public async void Run()
        {
            StreamReader r = new StreamReader("config.json");
            string jsonString = r.ReadToEnd();
            models.Config config = JsonConvert.DeserializeObject<models.Config>(jsonString);
            var adapterFactory = new AdapterFactory();
            var quoteProcessor = new QueueProcessor(adapterFactory);
            var result = await quoteProcessor.QueueProcessAsync(config);
            Console.WriteLine(result);
        }
    }
}
