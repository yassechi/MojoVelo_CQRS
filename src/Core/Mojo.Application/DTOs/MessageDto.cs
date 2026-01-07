using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Mojo.Application.DTOs
{
    public class MessageAddDto
    {
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }
        public string UserId { get; set; } = null!;
        public int DiscussionId { get; set; }
    }

    public class MessageUpdateDto
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }
        public string UserId { get; set; } = null!;
        public int DiscussionId { get; set; }

    }
}