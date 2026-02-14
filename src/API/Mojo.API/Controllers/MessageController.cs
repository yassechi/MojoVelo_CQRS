using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Message;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Messages.Request.Command;
using Mojo.Application.Features.Messages.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var messages = await _mediator.Send(new GetAllMessageRequest());
            return Ok(messages);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var message = await _mediator.Send(new GetMessageDetailsRequest { Id = id });
                return Ok(message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] MessageDto messageDto)
        {
            var response = await _mediator.Send(new CreateMessageCommand { dto = messageDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] MessageDto messageDto)
        {
            var response = await _mediator.Send(new UpdateMessageCommand { dto = messageDto });

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteMessageCommand { Id = id });

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("get-by-discussion/{discussionId}")]
        public async Task<IActionResult> GetByDiscussion(int discussionId)
        {
            var messages = await _mediator.Send(new GetMessagesByDiscussionRequest { DiscussionId = discussionId });
            return Ok(messages);
        }
    }
}