using Mojo.Application.Features.Amortissments.Request.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    internal class UpdateAmortissementHandler : IRequestHandler<UpdateAmortissementCommand, Unit>
    {
        private readonly IAmortissementRepository repository;
        private readonly IMapper mapper;

        public UpdateAmortissementHandler(IAmortissementRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateAmortissementCommand request, CancellationToken cancellationToken)
        {
            var oldAmortissement = await repository.GetByIdAsync(request.dto.Id);
            var updatedAmortissement = mapper.Map(request.dto, oldAmortissement);
            await repository.UpadteAsync(updatedAmortissement);
            return Unit.Value;
        }
    }
}
