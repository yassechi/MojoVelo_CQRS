namespace Mojo.Application.DTOs.EntitiesDto.MoisAmortissement
{
    public class MoisAmortissementDto : BaseEntity<int>
    {
        public int AmortissementId { get; set; }
        public int NumeroMois { get; set; }
        public decimal Montant { get; set; }
    }
}
