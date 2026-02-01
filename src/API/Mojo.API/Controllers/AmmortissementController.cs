using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Amortissement;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Amortissments.Request.Command;
using Mojo.Application.Features.Amortissments.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmortissementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AmortissementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var amortissements = await _mediator.Send(new GetAllAmortissementRequest());
            return Ok(amortissements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmortissement(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetAmortissementDetailsRequest { Id = id });
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AmortissmentDto amortissement)
        {
            var response = await _mediator.Send(new CreateAmortissementCommand { amortissmentDto = amortissement });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] AmortissmentDto amortissmentDto)
        {
            var response = await _mediator.Send(new UpdateAmortissementCommand { dto = amortissmentDto });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteAmortissementCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}