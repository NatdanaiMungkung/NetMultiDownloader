using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloader.Adapters
{
    internal interface IAdapterFactory
    {
        IDownloader GetDownloader(Uri uri);
    }
}
