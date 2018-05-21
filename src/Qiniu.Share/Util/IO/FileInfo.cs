using System;

namespace Qiniu.Share.IO
{
    public class FileInfo
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}