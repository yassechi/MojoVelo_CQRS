using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Amortissement;
using Mojo.Application.Features.Amortissments.Request.Command;
using Mojo.Application.Features.Amortissments.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmortissementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AmortissementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var amortissements = await _mediator.Send(new GetAllAmortissementRequest());
            return Ok(amortissements);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ammortissement = await _mediator.Send(new GetAmortissementDetailsRequest { Id = id });
            return Ok(ammortissement);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AmortissmentDto amotissement)
        {
            var response = await _mediator.Send(new CreateAmortissementCommand { AmortissmentDto = amotissement });
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] AmortissmentDto amortissmentDto)
        {
            var response = await _mediator.Send(new UpdateAmortissementCommand { dto = amortissmentDto });
            return Ok(response);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteAmortissementCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response); 
            }
            return Ok(response); 
        }

    }
}
