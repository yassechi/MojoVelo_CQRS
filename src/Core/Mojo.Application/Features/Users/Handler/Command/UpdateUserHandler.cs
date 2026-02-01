using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mojo.Application.DTOs.EntitiesDto.User.Validators;
using Mojo.Application.Features.Users.Request.Command;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IOrganisationRepository _organisationRepository;

        public UpdateUserHandler(
            UserManager<User> userManager,
            IMapper mapper,
            IOrganisationRepository organisationRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _organisationRepository = organisationRepository;
        }

        public async Task<BaseResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            // 1. Validation des données via FluentValidation
            var validator = new UserValidator(_organisationRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la modification : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            // 2. Récupération de l'utilisateur existant depuis la DB
            var user = await _userManager.FindByIdAsync(request.dto.Id);

            if (user == null)
            {
                response.Succes = false;
                response.Message = "Utilisateur introuvable.";
                response.Errors.Add($"Aucun utilisateur trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            // 3. Mapping des propriétés du DTO vers l'entité existante
            _mapper.Map(request.dto, user);

            // 4. Gestion du hachage du mot de passe
            if (!string.IsNullOrWhiteSpace(request.dto.Password))
            {
                // On utilise le PasswordHasher du UserManager pour transformer le texte clair en hash
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.dto.Password);
            }

            // 5. Sécurité : Correction du SecurityStamp
            // Si le stamp est nul (cas fréquent après un import SQL manuel), Identity lèvera une exception.
            // On le régénère donc avant l'update final.
            if (string.IsNullOrEmpty(user.SecurityStamp))
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            // 6. Persistance via UserManager (met à jour le mot de passe, le stamp, etc.)
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                response.Succes = false;
                response.Message = "Echec de la mise à jour dans la base de données.";
                response.Errors = result.Errors.Select(e => e.Description).ToList();
                return response;
            }

            response.Succes = true;
            response.Message = "L'utilisateur a été modifié avec succès.";
            response.StrId = user.Id;

            return response;
        }
    }
}