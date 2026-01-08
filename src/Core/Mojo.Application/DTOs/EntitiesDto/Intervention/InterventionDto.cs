namespace Mojo.Application.DTOs.EntitiesDto.Intervention
{
    //public class InterventionAddDto : BaseEntity<int>
    public class InterventionDto : BaseEntity<int>
    {
        public DateTime DateIntervention { get; set; }
        public string TypeIntervention { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cout { get; set; }
        public int VeloId { get; set; }
    }

    //public class InterventionUpdateDto : BaseEntity<int>
    //{
    //    public DateTime DateIntervention { get; set; }
    //    public string TypeIntervention { get; set; } = null!;
    //    public string Description { get; set; } = null!;
    //    public decimal Cout { get; set; }
    //    public int VeloId { get; set; }

    //}
}