namespace Mojo.Application.DTOs.EntitiesDto.Velo
{
    //public class VeloAddDto : BaseEntity<int>
    public class VeloDto : BaseEntity<int>
    {
        public string NumeroSerie { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public decimal PrixAchat { get; set; }
        public bool Status { get; set; }
    }

    //public class VeloUpdateDto : BaseEntity<int>
    //{
    //    public string NumeroSerie { get; set; } = null!;
    //    public string Marque { get; set; } = null!;
    //    public string Modele { get; set; } = null!;
    //    public decimal PrixAchat { get; set; }
    //    public bool Status { get; set; }

    //}
}