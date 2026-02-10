using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Domain.Entities
{
    public class Documents : BaseEntity<Amortissement>
    {
        [ForeignKey(nameof(Contrat))]
        public int ContratId { get; set; }
        public Contrat Contrat { get; set; } = null!;

        public byte[] Fichier { get; set; } = null!;

        public string NomFichier { get; set; } = string.Empty;

        public string TypeFichier { get; set; } = string.Empty;
    }
}
