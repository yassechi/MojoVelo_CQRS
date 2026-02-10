using Mojo.Application.DTOs.EntitiesDto.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.DTOs.EntitiesDto.Documents.Validators
{
    public class DocumentValidator : AbstractValidator<DocumentDto>
    {
        private readonly IContratRepository _contratRepository;

        public DocumentValidator(IContratRepository contratRepository)
        {
            _contratRepository = contratRepository;

            RuleFor(d => d.ContratId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.")
                .MustAsync(async (contratId, cancellationToken) =>
                {
                    var contrat = await _contratRepository.GetByIdAsync(contratId);
                    return contrat != null;
                })
                .WithMessage("Le contrat avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(d => d.Fichier)
                .NotEmpty()
                .WithMessage("Le fichier est requis.");

            RuleFor(d => d.NomFichier)
                .NotEmpty()
                .WithMessage("Le nom du fichier est requis.")
                .MaximumLength(255)
                .WithMessage("Le nom du fichier ne peut pas dépasser 255 caractères.");

            RuleFor(d => d.TypeFichier)
                .NotEmpty()
                .WithMessage("Le type de fichier est requis.")
                .Must(type => new[] { "pdf", "jpg", "jpeg", "png", "doc", "docx" }.Contains(type.ToLower()))
                .WithMessage("Type de fichier non autorisé. Types acceptés: pdf, jpg, jpeg, png, doc, docx.");

            RuleSet("Create", () =>
            {
            });

            RuleSet("Update", () =>
            {
                RuleFor(d => d.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID du document est requis pour la mise à jour.");
            });
        }
    }
}
