using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloader.Adapters
{
    internal class AdapterFactory : IAdapterFactory
    {
        public IDownloader GetDownloader(Uri uri)
        {
            switch (uri.Scheme)
            {
                case "http":
                case "https":
                    return new HTTPDownloader();
                case "ftp":
                    return new FTPDownloader();
                case "sftp":
                    return new SFTPDownloader();
                default:
                    return null;
            }
        }
    }
}
