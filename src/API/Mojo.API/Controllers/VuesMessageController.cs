using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.Features.VuesMessages.Request.Command;
using Mojo.Application.Features.VuesMessages.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuesMessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VuesMessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount(
            [FromQuery] string userId,
            [FromQuery] int role,
            [FromQuery] int? organisationId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "userId requis." });
            }

            var count = await _mediator.Send(new GetUnreadMessageCountRequest
            {
                UserId = userId,
                Role = role,
                OrganisationId = organisationId
            });

            return Ok(count);
        }

        [HttpGet("unread-discussions")]
        public async Task<IActionResult> GetUnreadDiscussions(
            [FromQuery] string userId,
            [FromQuery] int role,
            [FromQuery] int? organisationId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "userId requis." });
            }

            var discussions = await _mediator.Send(new GetUnreadDiscussionsRequest
            {
                UserId = userId,
                Role = role,
                OrganisationId = organisationId
            });

            return Ok(discussions);
        }

        [HttpPost("mark-read")]
        public async Task<IActionResult> MarkRead([FromBody] MarkMessagesReadCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
