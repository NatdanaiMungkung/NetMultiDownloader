using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetMultiDownloader;
using NetMultiDownloader.Adapters;
using NetMultiDownloader.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetMultiDownloaderUnitTest.Processor
{
    [TestClass]
    public class QueueProcessorTest
    {
        [TestMethod]
        public async Task WhenDownloadSuccessShouldReturnSuccessAsync()
        {
            var adapterFactoryMock = new Mock<IAdapterFactory>();
            var downloaderMock = new Mock<HTTPDownloader>();
            downloaderMock.Setup(a => a.DownloadAsync(It.IsAny<Uri>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new NetMultiDownloader.Models.Result
            {
                IsCompleted = true,
                FileName = "xxx.jpg",
                Error = null,
                Uri = new Uri("http://haha.com")
            });
            adapterFactoryMock.Setup(a => a.GetDownloader(It.IsAny<Uri>())).Returns(downloaderMock.Object);
            var processor = new QueueProcessor(adapterFactoryMock.Object);
            var config = new Config
            {
                RetriesNo= 1,
                Path = "D:\\",
                 Uris = new List<string> { "http://haha.com"},
            };
            var result = await processor.QueueProcessAsync(config);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(true, result[0].IsCompleted);
        }

        [TestMethod]
        public async Task WhenDownloadSuccessShouldReturnSuccessAsyncWithDistinct()
        {
            var adapterFactoryMock = new Mock<IAdapterFactory>();
            var downloaderMock = new Mock<HTTPDownloader>();
            downloaderMock.Setup(a => a.DownloadAsync(It.IsAny<Uri>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new NetMultiDownloader.Models.Result
            {
                IsCompleted = true,
                FileName = "xxx.jpg",
                Error = null,
                Uri = new Uri("http://haha.com")
            });
            adapterFactoryMock.Setup(a => a.GetDownloader(It.IsAny<Uri>())).Returns(downloaderMock.Object);
            var processor = new QueueProcessor(adapterFactoryMock.Object);
            var config = new Config
            {
                RetriesNo = 1,
                Path = "D:\\",
                Uris = new List<string> { "http://haha.com", "http://haha.com", "http://haha2.com" },
            };
            var result = await processor.QueueProcessAsync(config);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(true, result[0].IsCompleted);
        }

    }
}
