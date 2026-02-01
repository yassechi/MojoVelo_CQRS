using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Amortissement;
using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Features.Amortissments.Request.Command;
using Mojo.Application.Features.Amortissments.Request.Query;
using Mojo.Application.Features.Contrats.Request.Command;
using Mojo.Application.Features.Contrats.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContratController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var contrats = await mediator.Send(new GetAllContratRequest());
            return Ok(contrats);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contrat = await mediator.Send(new GetContratDetailsRequest { Id = id });
            return Ok(contrat);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ContratDto contratDto)
        {
            var response = await mediator.Send(new CreateContratCommand { dto = contratDto });
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ContratDto contratDto)
        {
            var response = await mediator.Send(new UpdateContratCommand { dto = contratDto });
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new DeleteContratCommand { Id = id });

            if (!response.Succes)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
