using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Amortissments.Request.Command
{
    internal class DeleteAmortissementCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
