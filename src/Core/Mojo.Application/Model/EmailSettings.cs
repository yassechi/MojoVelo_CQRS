using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Model
{
    public class EmailSettings
    {
        public string ApiKey { get; set; }
        public string FromAddress { get; set; } = null!;
        public string FromName { get; set; } = null!;
    }
}
