
using Autofac;
using NetMultiDownloader.Adapters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetMultiDownloader
{
    public class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<AdapterFactory>().As<IAdapterFactory>();
            builder.RegisterType<QueueProcessor>().As<IQueueProcessor>();
            return builder.Build();
        }
        public static async Task Main(string[] args)
        {
            await CompositionRoot().Resolve<Application>().Run();
        }
    }
}
