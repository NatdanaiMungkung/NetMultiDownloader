using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetMultiDownloader.Adapters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloaderUnitTest.Adapters
{
    [TestClass]
    public class AdapterFactoryTest
    {
        [TestMethod]
        [DataRow("http://test.com", "http")]
        [DataRow("https://test.com", "https")]
        [DataRow("ftp://test.com", "ftp")]
        [DataRow("sftp://test.com", "sftp")]
        [DataRow("xxx://xxx.xxx", "xxx")]
        public void WhenGetDownloaderReturnCorrectType(string uri, string protocol)
        {
            var factory = new AdapterFactory();
            var adapter = factory.GetDownloader(new Uri(uri));
            switch (protocol)
            {
                case "http":
                case "https":
                    Assert.IsTrue(adapter is HTTPDownloader);
                    return;
                case "ftp":
                    Assert.IsTrue(adapter is FTPDownloader);
                    return;
                case "sftp":
                    Assert.IsTrue(adapter is SFTPDownloader);
                    return;
                default:
                    Assert.IsNull(adapter);
                    return;
            }
        }
    }
}
