using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.DTOs.EntitiesDto.Demande.Validators
{
    public class DemandeValidator : AbstractValidator<DemandeDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVeloRepository _veloRepository;
        private readonly IDiscussionRepository _discussionRepository;

        public DemandeValidator(
            IUserRepository userRepository,
            IVeloRepository veloRepository,
            IDiscussionRepository discussionRepository)
        {
            _userRepository = userRepository;
            _veloRepository = veloRepository;
            _discussionRepository = discussionRepository;

            RuleFor(d => d.Status)
                .IsInEnum()
                .WithMessage("Le statut doit être une valeur valide (1-4).");

            RuleFor(d => d.IdUser)
                .NotEmpty()
                .WithMessage("L'utilisateur est requis.")
                .MustAsync(async (idUser, cancellationToken) =>
                {
                    var user = await _userRepository.GetUserByStringId(idUser);
                    return user != null && user.IsActif;
                })
                .WithMessage("L'utilisateur avec l'Id '{PropertyValue}' n'existe pas ou est inactif.");

            RuleFor(d => d.IdVelo)
                .GreaterThan(0)
                .WithMessage("L'ID du vélo doit être supérieur à 0.")
                .MustAsync(async (idVelo, cancellationToken) =>
                {
                    var velo = await _veloRepository.GetByIdAsync(idVelo);
                    return velo != null && velo.IsActif;
                })
                .WithMessage("Le vélo avec l'Id {PropertyValue} n'existe pas ou est inactif.");

            RuleFor(d => d.DiscussionId)
                .MustAsync(async (discussionId, cancellationToken) =>
                {
                    if (discussionId == 0)
                        return true;

                    var discussion = await _discussionRepository.GetByIdAsync(discussionId);
                    return discussion != null && discussion.IsActif;
                })
                .WithMessage("La discussion avec l'Id {PropertyValue} n'existe pas ou est inactive.");

            RuleSet("Create", () =>
            {
                // Règles spécifiques à la création si nécessaire
            });

            RuleSet("Update", () =>
            {
                RuleFor(d => d.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID de la demande est requis pour la mise à jour.");
            });
        }
    }
}