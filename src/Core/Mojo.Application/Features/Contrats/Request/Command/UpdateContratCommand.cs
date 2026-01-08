using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Contrats.Request.Command
{
    internal class UpdateContratCommand : IRequest<Unit>
    {
        public ContratDto dto { get; set; }
    }
}
