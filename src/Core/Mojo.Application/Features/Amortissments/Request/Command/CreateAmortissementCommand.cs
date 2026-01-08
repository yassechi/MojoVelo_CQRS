using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Amortissments.Request.Command
{
    public class CreateAmortissementCommand : IRequest<Unit>
    {
        public AmortissmentDto amortissmentDto { get; set; }
    }
}
