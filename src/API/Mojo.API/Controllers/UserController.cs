using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Users.Request.Command;
using Mojo.Application.Features.Users.Request.Query;

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
        public async Task<IActionResult> Get()
        {
            var users = await _mediator.Send(new GetAllUserRequest());
            return Ok(users);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(string id)
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

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] UserDto userDto)
        {
            var response = await _mediator.Send(new CreateUserCommand { dto = userDto });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            var response = await _mediator.Send(new UpdateUserCommand { dto = userDto });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteUserCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}