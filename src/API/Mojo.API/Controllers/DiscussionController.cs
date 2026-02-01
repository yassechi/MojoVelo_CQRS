using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.DTOs.EntitiesDto.Discussion;
using Mojo.Application.Features.Contrats.Request.Command;
using Mojo.Application.Features.Contrats.Request.Query;
using Mojo.Application.Features.Discussions.Request.Command;
using Mojo.Application.Features.Discussions.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly IMediator mediator;

        public DiscussionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var discussions = await mediator.Send(new GetAllDiscussionRequest());
            return Ok(discussions);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var discussion = await mediator.Send(new GetDiscussionDetailsRequest { Id = id });
            return Ok(discussion);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] DiscussionDto discussionDto)
        {
            var response = await mediator.Send(new CreateDiscussionCommand { dto = discussionDto });
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DiscussionDto discussionDto)
        {
            var response = await mediator.Send(new UpdateDiscussionCommand { dto = discussionDto });
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new DeleteDiscussionCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
