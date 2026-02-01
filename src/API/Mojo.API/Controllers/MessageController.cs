using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Message;
using Mojo.Application.Features.Amortissments.Request.Command;
using Mojo.Application.Features.Messages.Request.Command;
using Mojo.Application.Features.Messages.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var messages = await mediator.Send(new GetAllMessageRequest());
            return Ok(messages);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var message = await mediator.Send(new GetMessageDetailsRequest { Id = id });
            return Ok(message);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] MessageDto messageDto)
        {
            var response = await mediator.Send(new CreateMessageCommand { dto = messageDto });
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] MessageDto messageDto)
        {
            var response = await mediator.Send(new UpdateMessageCommand { dto = messageDto });
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new DeleteMessageCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

