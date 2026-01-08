using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Messages.Request.Command
{
    internal class CreateMessageCommand :  IRequest<Unit>
    {
        public MessageDto dto { get; set; }
    }
}
