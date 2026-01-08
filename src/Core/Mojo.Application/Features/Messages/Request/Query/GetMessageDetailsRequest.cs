using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Messages.Request.Query
{
    internal class GetMessageDetailsRequest : IRequest<MessageDto>
    {
        public int Id { get; set; }
    }
}
