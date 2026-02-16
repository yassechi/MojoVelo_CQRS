using Mojo.Domain.Enums;

namespace Mojo.Application.DTOs.EntitiesDto.Contrat
{
    public class ContratDetailDto
    {
        public int Id { get; set; }
        public string Ref { get; set; } = string.Empty;
        public int VeloId { get; set; }
        public string VeloMarque { get; set; } = string.Empty;
        public string VeloModele { get; set; } = string.Empty;
        public string? VeloNumeroSerie { get; set; }
        public string BeneficiaireId { get; set; } = string.Empty;
        public string BeneficiaireName { get; set; } = string.Empty;
        public string UserRhId { get; set; } = string.Empty;
        public string UserRhName { get; set; } = string.Empty;
        public DateOnly DateDebut { get; set; }
        public DateOnly DateFin { get; set; }
        public int Duree { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public StatutContrat StatutContrat { get; set; }
    }
}
