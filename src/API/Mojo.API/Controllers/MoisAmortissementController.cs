using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.MoisAmortissements.Request.Command;
using Mojo.Application.Features.MoisAmortissements.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoisAmortissementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoisAmortissementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var items = await _mediator.Send(new GetAllMoisAmortissementRequest());
            return Ok(items);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var item = await _mediator.Send(new GetMoisAmortissementDetailsRequest { Id = id });
                return Ok(item);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-amortissement/{amortissementId}")]
        public async Task<IActionResult> GetByAmortissement(int amortissementId)
        {
            var items = await _mediator.Send(new GetMoisAmortissementsByAmortissementRequest
            {
                AmortissementId = amortissementId
            });
            return Ok(items);
        }

        [HttpGet("get-by-contrat/{contratId}")]
        public async Task<IActionResult> GetByContrat(int contratId)
        {
            var items = await _mediator.Send(new GetMoisAmortissementsByContratRequest
            {
                ContratId = contratId
            });
            return Ok(items);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] MoisAmortissementDto dto)
        {
            var response = await _mediator.Send(new CreateMoisAmortissementCommand { dto = dto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] MoisAmortissementDto dto)
        {
            var response = await _mediator.Send(new UpdateMoisAmortissementCommand { dto = dto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteMoisAmortissementCommand { Id = id });

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
