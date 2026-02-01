using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Contrats.Request.Command;
using Mojo.Application.Features.Contrats.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContratController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var contrats = await _mediator.Send(new GetAllContratRequest());
            return Ok(contrats);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var contrat = await _mediator.Send(new GetContratDetailsRequest { Id = id });
                return Ok(contrat);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ContratDto contratDto)
        {
            var response = await _mediator.Send(new CreateContratCommand { dto = contratDto });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ContratDto contratDto)
        {
            var response = await _mediator.Send(new UpdateContratCommand { dto = contratDto });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteContratCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}