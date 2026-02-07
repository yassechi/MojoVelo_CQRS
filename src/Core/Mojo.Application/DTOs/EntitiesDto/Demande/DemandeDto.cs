using Mojo.Application.DTOs.Common;
using Mojo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.DTOs.EntitiesDto.Demande
{
    public class DemandeDto : BaseDto<int>
    {

        [Required]
        public DemandeStatus Status { get; set; }

        [Required]
        public string IdUser { get; set; } = null!;

        [Required]
        public int IdVelo { get; set; }

        public int DiscussionId { get; set; }

    }
}
