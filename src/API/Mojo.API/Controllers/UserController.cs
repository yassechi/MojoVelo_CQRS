using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.API.Attributes;
using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Users.Request.Command;
using Mojo.Application.Features.Users.Request.Query;
using Mojo.Domain.Enums;
using System.Security.Claims;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        [AuthorizeRole(UserRole.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new GetAllUserRequest());
            return Ok(users);
        }

        [HttpGet("get-one/{id}")]
        //[AuthorizeRole(UserRole.Admin, UserRole.Manager, UserRole.User)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserDetailsRequest { Id = id });
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("me")]
        [AuthorizeRole(UserRole.Admin, UserRole.Manager, UserRole.User)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst("uid")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Utilisateur non authentifié." });
            }

            try
            {
                var user = await _mediator.Send(new GetUserDetailsRequest { Id = userId });
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        [AuthorizeRole(UserRole.Admin, UserRole.Manager, UserRole.User)]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            var userId = User.FindFirst("uid")?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "1" && userId != userDto.Id)
            {
                return Forbid();
            }

            var response = await _mediator.Send(new UpdateUserCommand { dto = userDto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        [AuthorizeRole(UserRole.Admin)]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteUserCommand { Id = id });
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}