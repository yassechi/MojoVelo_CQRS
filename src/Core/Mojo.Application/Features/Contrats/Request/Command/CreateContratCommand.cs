using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Contrats.Request.Command
{
    public class CreateContratCommand : IRequest
    {
        public ContratDto dto { get; set; }
    }
}
