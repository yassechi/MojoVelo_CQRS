using AutoMapper;
using MediatR;
using Mojo.Application.Features.Contrats.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class CreateContratHandler : IRequestHandler<CreateContratCommand, Unit>
    {
        private readonly IContratRepository repository;
        private readonly IMapper mapper;

        public CreateContratHandler(IContratRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateContratCommand request, CancellationToken cancellationToken)
        {

            var contrat = mapper.Map<Mojo.Domain.Entities.Contrat>(request.dto);

            await repository.CreateAsync(contrat);

            return Unit.Value;
        }
    }
}