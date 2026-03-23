using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Domain.AI
{

    public class RagPdfInfo
    {
        public string FileName { get; set; } = string.Empty;
        public long SizeBytes { get; set; }
        public DateTime LastModifiedUtc { get; set; }
        public string SizeMb => $"{SizeBytes / 1024.0 / 1024.0:F2} MB";
    }
}
