using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Features.Demandes.Request.Query;
using System.Text;

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

        [HttpGet("get-detail/{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var demande = await _mediator.Send(new GetDemandeDetailViewRequest { Id = id });
                return Ok(demande);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var demandes = await _mediator.Send(new GetDemandesByUserRequest { UserId = userId });
            return Ok(demandes);
        }

        [HttpGet("get-by-organisation/{organisationId}")]
        public async Task<IActionResult> GetByOrganisation(int organisationId)
        {
            var demandes = await _mediator.Send(new GetDemandesByOrganisationRequest { OrganisationId = organisationId });
            return Ok(demandes);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(
            [FromQuery] int? status,
            [FromQuery] string? type,
            [FromQuery] string? search,
            [FromQuery] int? organisationId,
            [FromQuery] string? userId)
        {
            var demandes = await _mediator.Send(new GetDemandeListRequest
            {
                Status = status,
                Type = type,
                Search = search,
                OrganisationId = organisationId,
                UserId = userId
            });
            return Ok(demandes);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(
            [FromQuery] int? status,
            [FromQuery] string? type,
            [FromQuery] string? search,
            [FromQuery] int? organisationId,
            [FromQuery] string? userId)
        {
            var csv = await _mediator.Send(new GetDemandeExportRequest
            {
                Status = status,
                Type = type,
                Search = search,
                OrganisationId = organisationId,
                UserId = userId
            });

            var bytes = new UTF8Encoding(true).GetBytes(csv);
            return File(bytes, "text/csv", "demandes-export.csv");
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

        [HttpPost("create-with-bike")]
        public async Task<IActionResult> CreateWithBike([FromBody] CreateDemandeWithBikeDto payload)
        {
            var response = await _mediator.Send(new CreateDemandeWithBikeCommand { dto = payload });
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
