using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Mojo.Application.DTOs
{
    public class DiscussionAddDto
    {
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }
        public string ClientId { get; set; } = null!;
        public string MojoId { get; set; } = null!;
    }

    public class DiscussionUpdateDto
    {
        public int Id { get; set; }
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }
        public string ClientId { get; set; } = null!;
        public string MojoId { get; set; } = null!;
    }
}