using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;
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

        
    }
}
