using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Intervention;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Interventions.Request.Command;
using Mojo.Application.Features.Interventions.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InterventionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var interventions = await _mediator.Send(new GetAllInterventionRequest());
            return Ok(interventions);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var intervention = await _mediator.Send(new GetInterventionDetailsRequest { Id = id });
                return Ok(intervention);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InterventionDto interventionDto)
        {
            var response = await _mediator.Send(new CreateInterventionCommand { dto = interventionDto });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InterventionDto interventionDto)
        {
            var response = await _mediator.Send(new UpdateInterventionCommand { dto = interventionDto });

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteInterventionCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}