using System;
using System.Threading.Tasks;

namespace Mojo.Application.Persistance.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        // --- Tables Identité & Utilisateurs ---
        IOrganisationRepository OrganisationRepository { get; }
        // Note: Le UserManager gère souvent les Users, mais on peut avoir un Repo pour les lectures complexes

        // --- Tables Métier (Vélos & Gestion) ---
        IVeloRepository VeloRepository { get; }
        IContratRepository ContratRepository { get; }
        IInterventionRepository InterventionRepository { get; }
        IAmortissementRepository AmortissementRepository { get; }
        IMoisAmortissementRepository MoisAmortissementRepository { get; }

        // --- Tables SAV / Communication ---
        IDiscussionRepository DiscussionRepository { get; }
        IMessageRepository MessageRepository { get; }

        // --- Méthode de validation globale ---
        Task<int> Save();
    }
}
