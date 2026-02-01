using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;

namespace Mojo.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MDbContext _context;

        // On prépare des champs privés pour stocker les instances (Lazy Loading)
        private IOrganisationRepository? _organisationRepository;
        private IVeloRepository? _veloRepository;
        private IContratRepository? _contratRepository;
        private IInterventionRepository? _interventionRepository;
        private IAmortissementRepository? _amortissementRepository;
        private IDiscussionRepository? _discussionRepository;
        private IMessageRepository? _messageRepository;

        public UnitOfWork(MDbContext context)
        {
            _context = context;
        }

        // Si le repository est nul, on l'instancie en lui passant le context unique [cite: 2026-01-31]
        public IOrganisationRepository OrganisationRepository =>
            _organisationRepository ??= new OrganisationRepository(_context);

        public IVeloRepository VeloRepository =>
            _veloRepository ??= new VeloRepository(_context);

        public IContratRepository ContratRepository =>
            _contratRepository ??= new ContratRepository(_context);

        public IInterventionRepository InterventionRepository =>
            _interventionRepository ??= new InterventionRepository(_context);

        public IAmortissementRepository AmortissementRepository =>
            _amortissementRepository ??= new AmortissementRepository(_context);

        public IDiscussionRepository DiscussionRepository =>
            _discussionRepository ??= new DiscussionRepository(_context);

        public IMessageRepository MessageRepository =>
            _messageRepository ??= new MessageRepository(_context);

        // Cette méthode valide TOUS les changements en une seule transaction SQL [cite: 2026-01-31]
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}