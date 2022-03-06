using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetMultiDownloader;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloaderUnitTest.Helpers
{
    [TestClass]
    public class FileNameHelperTest
    {
        [TestMethod]
        [DataRow("sftp://pinti:Pax038764269@localhost/C%3A/Users/pinti/Downloads/1.jpg", "1-3307CD.jpg")]
        [DataRow("sftp://pinti:Pax038764269@localhost/C%3A/Users/pinti/Downloads/1/1.jpg", "1-0CC89C.jpg")]
        public void WhenGetDeDupFileName_Should_Return_ValidResult(string uri, string filename)
        {
            Assert.AreEqual(FileNameHelper.GetDeDupFileName(new Uri(uri)), filename);
        }
    }
}
