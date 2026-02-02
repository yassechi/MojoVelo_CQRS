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

            var validator = new UserValidator(_organisationRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var user = await _userManager.FindByIdAsync(request.dto.Id);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Utilisateur introuvable.";
                response.Errors.Add($"Aucun utilisateur trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, user);

            if (!string.IsNullOrWhiteSpace(request.dto.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.dto.Password);
            }

            if (string.IsNullOrEmpty(user.SecurityStamp))
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = "Echec de la mise à jour dans la base de données.";
                response.Errors = result.Errors.Select(e => e.Description).ToList();
                return response;
            }

            response.Success = true;
            response.Message = "L'utilisateur a été modifié avec succès.";
            response.StrId = user.Id;
            return response;
        }
    }
}