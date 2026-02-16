namespace Mojo.Domain.Entities
{
    public class Velo : BaseEntity<Velo>
    {
        public string NumeroSerie { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public string? Type { get; set; }
        public decimal PrixAchat { get; set; }
        public bool Status { get; set; }
        public List<Intervention> Interventions { get; set; } = [];
        public List<Amortissement> Amortissements { get; set; } = [];
        public List<Contrat> Contrats { get; set; } = [];
    }
}
