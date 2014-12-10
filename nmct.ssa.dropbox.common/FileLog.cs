using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.dropbox.common
{
    public class FileLog
    {
        public int Id { get; set; }
        public DateTime TimeDownloaded { get; set; }
        public string DownloadBy { get; set; }
        public int FileId { get; set; }
    }
}
