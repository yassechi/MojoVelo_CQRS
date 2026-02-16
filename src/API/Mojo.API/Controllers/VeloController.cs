using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Velo;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Velos.Request.Command;
using Mojo.Application.Features.Velos.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeloController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VeloController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var velos = await _mediator.Send(new GetAllVeloRequest());
            return Ok(velos);
        }

        [HttpGet("get-types")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _mediator.Send(new GetVeloTypesRequest());
            return Ok(types);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var velo = await _mediator.Send(new GetVeloDetailsRequest { Id = id });
                return Ok(velo);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] VeloDto veloDto)
        {
            var response = await _mediator.Send(new CreateVeloCommand { dto = veloDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] VeloDto veloDto)
        {
            var response = await _mediator.Send(new UpdateVeloCommand { dto = veloDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteVeloCommand { Id = id });

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
