using Mojo.Domain.Enums;

namespace Mojo.Application.DTOs.EntitiesDto.Demande
{
    public class DemandeDetailDto
    {
        public int Id { get; set; }
        public DemandeStatus Status { get; set; }
        public string IdUser { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int IdVelo { get; set; }
        public string VeloMarque { get; set; } = string.Empty;
        public string VeloModele { get; set; } = string.Empty;
        public string? VeloType { get; set; }
        public decimal? VeloPrixAchat { get; set; }
        public int DiscussionId { get; set; }
        public int? VeloCmsId { get; set; }
        public List<DemandeMessageDto> Messages { get; set; } = new();
    }

    public class DemandeMessageDto
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = string.Empty;
        public DateTime? DateEnvoi { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string RoleLabel { get; set; } = string.Empty;
    }
}
