using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Mojo.Application.DTOs
{
    public class ContratAddDto
    {
        [Column(TypeName = "date")]
        public DateOnly DateDebut { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateFin { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public bool StatutContrat { get; set; }
        public int VeloId { get; set; }
        public int BeneficiaireId { get; set; }
        public int UserRhId { get; set; }
    }

    public class ContratUpdateDto
    {
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateOnly DateDebut { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateFin { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public bool StatutContrat { get; set; }
        public int VeloId { get; set; }
        public int BeneficiaireId { get; set; }
        public int UserRhId { get; set; }

    }
}