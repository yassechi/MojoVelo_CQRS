using Mojo.Application.DTOs.EntitiesDto.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Messages.Request.Query
{
    public class GetMessagesByDiscussionRequest : IRequest<List<MessageDto>>
    {
        public int DiscussionId { get; set; }
    }
}
