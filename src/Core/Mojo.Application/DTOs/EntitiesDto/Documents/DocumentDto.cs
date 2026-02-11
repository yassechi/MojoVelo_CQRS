using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Application.DTOs.EntitiesDto.Documents
{
    public class DocumentDto : BaseEntity<int>
    {
        [ForeignKey(nameof(Contrat))]
        public int ContratId { get; set; }

        public byte[] Fichier { get; set; } = null!;

        //public IFormFile File { get; set; }

        public string NomFichier { get; set; } = string.Empty;

        public string TypeFichier { get; set; } = string.Empty; 
    }
}