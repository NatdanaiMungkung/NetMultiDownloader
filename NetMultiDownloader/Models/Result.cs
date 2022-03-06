using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloader.Models
{
    internal class Result
    {
        public Result(Uri uri)
        {
            IsCompleted = true;
            Uri = uri;
        }
        public bool IsCompleted { get; set; }
        public Uri Uri { get; set; }
        public string Error { get; set; }

        public string FileName { get; set; }
    }
}
