using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Features.Demandes.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DemandeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var demandes = await _mediator.Send(new GetAllDemandeRequest());
            return Ok(demandes);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var demande = await _mediator.Send(new GetDemandeDetailsRequest { Id = id });
                return Ok(demande);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] DemandeDto demandeDto)
        {
            var response = await _mediator.Send(new CreateDemandeCommand { dto = demandeDto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DemandeDto demandeDto)
        {
            var response = await _mediator.Send(new UpdateDemandeCommand { dto = demandeDto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteDemandeCommand { Id = id });
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateDemandeStatusCommand statusDto)
        {
            var response = await _mediator.Send(new UpdateDemandeStatusCommand { Id = id, Status = statusDto.Status });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}