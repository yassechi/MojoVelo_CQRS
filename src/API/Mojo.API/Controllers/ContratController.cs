using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Contrats.Request.Command;
using Mojo.Application.Features.Contrats.Request.Query;
using System.Text;

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

        [HttpGet("get-detail/{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var contrat = await _mediator.Send(new GetContratDetailViewRequest { Id = id });
                return Ok(contrat);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("edit-data/{id}")]
        public async Task<IActionResult> GetEditData(int id)
        {
            try
            {
                var data = await _mediator.Send(new GetContratEditDataRequest { Id = id });
                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var contrats = await _mediator.Send(new GetContratsByUserRequest { UserId = userId });
            return Ok(contrats);
        }

        [HttpGet("get-by-organisation/{organisationId}")]
        public async Task<IActionResult> GetByOrganisation(int organisationId)
        {
            var contrats = await _mediator.Send(new GetContratsByOrganisationRequest { OrganisationId = organisationId });
            return Ok(contrats);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(
            [FromQuery] string? type,
            [FromQuery] string? search,
            [FromQuery] bool? endingSoon,
            [FromQuery] bool? withIncidents,
            [FromQuery] int? organisationId,
            [FromQuery] string? userId)
        {
            var contrats = await _mediator.Send(new GetContratListRequest
            {
                Type = type,
                Search = search,
                EndingSoon = endingSoon,
                WithIncidents = withIncidents,
                OrganisationId = organisationId,
                UserId = userId
            });
            return Ok(contrats);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(
            [FromQuery] string? type,
            [FromQuery] string? search,
            [FromQuery] bool? endingSoon,
            [FromQuery] bool? withIncidents,
            [FromQuery] int? organisationId,
            [FromQuery] string? userId)
        {
            var csv = await _mediator.Send(new GetContratExportRequest
            {
                Type = type,
                Search = search,
                EndingSoon = endingSoon,
                WithIncidents = withIncidents,
                OrganisationId = organisationId,
                UserId = userId
            });

            var bytes = new UTF8Encoding(true).GetBytes(csv);
            return File(bytes, "text/csv", "contrats-export.csv");
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ContratDto contratDto)
        {
            var response = await _mediator.Send(new CreateContratCommand { dto = contratDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ContratDto contratDto)
        {
            var response = await _mediator.Send(new UpdateContratCommand { dto = contratDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteContratCommand { Id = id });

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
