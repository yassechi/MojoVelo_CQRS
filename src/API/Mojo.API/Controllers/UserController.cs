using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.Features.Amortissments.Request.Command;
using Mojo.Application.Features.Users.Request.Command;
using Mojo.Application.Features.Users.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var users = await mediator.Send(new GetAllUserRequest());
            return Ok(users);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await mediator.Send(new GetUserDetailsRequest { Id = id });
            return Ok(user);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] UserDto userDto)
        {
            var response = await mediator.Send(new CreateUserCommand { dto = userDto });
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            var response = await mediator.Send(new UpdateUserCommand { dto = userDto });
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new DeleteUserCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
