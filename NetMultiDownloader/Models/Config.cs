using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloader.models
{
    public class Config
    {
        public int RetriesNo { get; set; }
        public string Path { get; set; }
        public List<string> Uris { get; set; }
    }
}
