using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Mojo.Application.DTOs
{
    public class VeloAddDto
    {
        public string NumeroSerie { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public decimal PrixAchat { get; set; }
        public bool Status { get; set; }
    }

    public class VeloUpdateDto
    {
        public int Id { get; set; }
        public string NumeroSerie { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public decimal PrixAchat { get; set; }
        public bool Status { get; set; }

    }
}