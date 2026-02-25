namespace Mojo.Application.DTOs.EntitiesDto.OrganisationLogo
{
    public class OrganisationLogoDto : BaseEntity<int>
    {
        public int OrganisationId { get; set; }

        public byte[] Fichier { get; set; } = null!;

        public string NomFichier { get; set; } = string.Empty;

        public string TypeFichier { get; set; } = string.Empty;
    }
}
