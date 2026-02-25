using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.OrganisationLogos.Request.Command;
using Mojo.Application.Features.OrganisationLogos.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationLogoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganisationLogoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var logos = await _mediator.Send(new GetAllOrganisationLogoRequest());
            return Ok(logos);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var logo = await _mediator.Send(new GetOrganisationLogoDetailsRequest { Id = id });
                return Ok(logo);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-organisation/{organisationId}")]
        public async Task<IActionResult> GetByOrganisation(int organisationId)
        {
            var logos = await _mediator.Send(new GetOrganisationLogosByOrganisationRequest { OrganisationId = organisationId });
            return Ok(logos);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] OrganisationLogoDto logoDto)
        {
            var response = await _mediator.Send(new CreateOrganisationLogoCommand { dto = logoDto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] OrganisationLogoDto logoDto)
        {
            var response = await _mediator.Send(new UpdateOrganisationLogoCommand { dto = logoDto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteOrganisationLogoCommand { Id = id });
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
