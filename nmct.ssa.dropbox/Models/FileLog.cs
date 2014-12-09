using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.dropbox.Models
{
    public class FileLog
    {
        public int Id { get; set; }
        public DateTime TimeDownloaded { get; set; }
        public string DownloadBy { get; set; }
        public int FileId { get; set; }
    }
}