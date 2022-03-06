
using Autofac;
using NetMultiDownloader.Adapters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetMultiDownloader
{
    internal class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            return builder.Build();
        }
        static async Task Main(string[] args)
        {
            CompositionRoot().Resolve<Application>().Run();
        }
    }
}
