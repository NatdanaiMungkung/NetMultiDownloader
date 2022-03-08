using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloader.Adapters
{
    public interface IAdapterFactory
    {
        public IDownloader GetDownloader(Uri uri);
    }
}
