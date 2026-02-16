using Mojo.Domain.Enums;

namespace Mojo.Application.DTOs.EntitiesDto.Demande
{
    public class AdminDemandeListItemDto
    {
        public int Id { get; set; }
        public DemandeStatus Status { get; set; }
        public string IdUser { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; } = string.Empty;
        public int IdVelo { get; set; }
        public string VeloMarque { get; set; } = string.Empty;
        public string VeloModele { get; set; } = string.Empty;
        public string? VeloType { get; set; }
        public decimal? VeloPrixAchat { get; set; }
        public int DiscussionId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
