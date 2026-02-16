namespace Mojo.Application.DTOs.Dashboard
{
    public class UserDashboardDto
    {
        public int TotalDemandes { get; set; }
        public int TotalContrats { get; set; }
        public int DemandesEnCours { get; set; }
        public int ContratsActifs { get; set; }
        public int DemandesAttente { get; set; }
        public int DemandesAttenteCompagnie { get; set; }
        public int DemandesValide { get; set; }
        public int ContratsEnCours { get; set; }
        public int ContratsTermine { get; set; }
    }
}
