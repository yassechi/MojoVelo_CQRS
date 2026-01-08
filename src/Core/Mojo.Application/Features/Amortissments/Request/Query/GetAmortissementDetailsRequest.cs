using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Amortissments.Request.Query
{
    public class GetAmortissementDetailsRequest : IRequest<AmortissmentDto>
    {
        public int Id { get; set; }
    }
}
