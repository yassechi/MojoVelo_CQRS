using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Organisation;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Organisations.Request.Command;
using Mojo.Application.Features.Organisations.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganisationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var organisations = await _mediator.Send(new GetAllOrganisationRequest());
            return Ok(organisations);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] bool? isActif, [FromQuery] string? search)
        {
            var organisations = await _mediator.Send(new GetOrganisationListRequest
            {
                IsActif = isActif,
                Search = search
            });
            return Ok(organisations);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var organisation = await _mediator.Send(new GetOrganisationDetailsRequest { Id = id });
                return Ok(organisation);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("resolve")]
        public async Task<IActionResult> Resolve([FromQuery] string emailOrDomain)
        {
            var organisation = await _mediator.Send(new ResolveOrganisationRequest
            {
                EmailOrDomain = emailOrDomain
            });
            return Ok(organisation);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] OrganisationDto organisationDto)
        {
            var response = await _mediator.Send(new CreateOrganisationCommand { dto = organisationDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] OrganisationDto organisationDto)
        {
            var response = await _mediator.Send(new UpdateOrganisationCommand { dto = organisationDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteOrganisationCommand { Id = id });

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
