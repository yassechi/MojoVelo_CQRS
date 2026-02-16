namespace Mojo.Application.DTOs.EntitiesDto.Demande
{
    public class CreateDemandeWithBikeDto
    {
        public string IdUser { get; set; } = null!;
        public string? MojoId { get; set; }
        public BikeSnapshotDto Bike { get; set; } = null!;
    }

    public class BikeSnapshotDto
    {
        public int CmsId { get; set; }
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public string? Type { get; set; }
        public decimal PrixAchat { get; set; }
    }
}
