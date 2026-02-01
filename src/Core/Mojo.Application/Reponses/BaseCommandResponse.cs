using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Reponses
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public bool Succes { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public string StrId { get; set; }
    }
}
