using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Interventions.Handler.Command
{
    internal class UpdateInterventionHandler : IRequestHandler<UpdateInterventionCommand, Unit>
    {
        private readonly IInterventionRepository repository;
        private readonly IMapper mapper;

        public UpdateInterventionHandler(IInterventionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateInterventionCommand request, CancellationToken cancellationToken)
        {
            var oldIntervention = await repository.GetByIdAsync(request.dto.Id);
            var updatedIntervention = mapper.Map(request.dto, oldIntervention);
            await repository.UpadteAsync(updatedIntervention);
            return Unit.Value;
        }
    }
}
