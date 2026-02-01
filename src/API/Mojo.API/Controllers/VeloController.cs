using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Velo;
using Mojo.Application.Features.Discussions.Request.Command;
using Mojo.Application.Features.Velos.Request.Command;
using Mojo.Application.Features.Velos.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeloController : ControllerBase
    {
        private readonly IMediator mediator;

        public VeloController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var velos = await mediator.Send(new GetAllVeloRequest());
            return Ok(velos);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var velo = await mediator.Send(new GetVeloDetailsRequest { Id = id });
            return Ok(velo);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] VeloDto veloDto)
        {
            var response = await mediator.Send(new CreateVeloCommand { dto = veloDto });
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] VeloDto veloDto)
        {
            var response = await mediator.Send(new UpdateVeloCommand { dto = veloDto });
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new DeleteVeloCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

