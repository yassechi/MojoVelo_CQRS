using Mojo.Domain.Enums;

namespace Mojo.Application.DTOs.EntitiesDto.Contrat
{
    public class AdminContratListItemDto
    {
        public int Id { get; set; }
        public string Ref { get; set; } = string.Empty;
        public string BeneficiaireId { get; set; } = string.Empty;
        public string BeneficiaireName { get; set; } = string.Empty;
        public string UserRhId { get; set; } = string.Empty;
        public string UserRhName { get; set; } = string.Empty;
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; } = string.Empty;
        public int VeloId { get; set; }
        public string VeloMarque { get; set; } = string.Empty;
        public string VeloModele { get; set; } = string.Empty;
        public string? VeloType { get; set; }
        public decimal VeloPrixAchat { get; set; }
        public DateOnly DateDebut { get; set; }
        public DateOnly DateFin { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public int Duree { get; set; }
        public StatutContrat StatutContrat { get; set; }
        public int IncidentsCount { get; set; }
        public decimal MaintenanceBudget { get; set; }
        public decimal MaintenanceUsed { get; set; }
        public int MaintenanceProgress { get; set; }
        public bool IsEndingSoon { get; set; }
    }
}
