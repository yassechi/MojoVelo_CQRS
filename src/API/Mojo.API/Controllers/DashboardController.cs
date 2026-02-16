using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.Features.Dashboard.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdmin()
        {
            var dashboard = await _mediator.Send(new GetAdminDashboardRequest());
            return Ok(dashboard);
        }

        [HttpGet("manager/{organisationId}")]
        public async Task<IActionResult> GetManager(int organisationId)
        {
            var dashboard = await _mediator.Send(new GetManagerDashboardRequest { OrganisationId = organisationId });
            return Ok(dashboard);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var dashboard = await _mediator.Send(new GetUserDashboardRequest { UserId = userId });
            return Ok(dashboard);
        }
    }
}
