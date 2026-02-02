using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Discussion;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Discussions.Request.Command;
using Mojo.Application.Features.Discussions.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiscussionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var discussions = await _mediator.Send(new GetAllDiscussionRequest());
            return Ok(discussions);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var discussion = await _mediator.Send(new GetDiscussionDetailsRequest { Id = id });
                return Ok(discussion);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] DiscussionDto discussionDto)
        {
            var response = await _mediator.Send(new CreateDiscussionCommand { dto = discussionDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DiscussionDto discussionDto)
        {
            var response = await _mediator.Send(new UpdateDiscussionCommand { dto = discussionDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteDiscussionCommand { Id = id });

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}