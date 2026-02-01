using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Organisation;
using Mojo.Application.Features.Amortissments.Request.Command;
using Mojo.Application.Features.Organisations.Request.Command;
using Mojo.Application.Features.Organisations.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrganisationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var organisations = await mediator.Send(new GetAllOrganisationRequest());
            return Ok(organisations);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var organisation = await mediator.Send(new GetOrganisationDetailsRequest { Id = id });
            return Ok(organisation);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] OrganisationDto organisationDto)
        {
            var response = await mediator.Send(new CreateOrganisationCommand { dto = organisationDto });
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] OrganisationDto organisationDto)
        {
            var response = await mediator.Send(new UpdateOrganisationCommand { dto = organisationDto });
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new DeleteOrganisationCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
