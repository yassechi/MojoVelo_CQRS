using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mojo.Application.DTOs.EntitiesDto.Documents;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Documents.Request.Command;
using Mojo.Application.Features.Documents.Request.Query;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var documents = await _mediator.Send(new GetAllDocumentRequest());
            return Ok(documents);
        }

        [HttpGet("get-one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var document = await _mediator.Send(new GetDocumentDetailsRequest { Id = id });
                return Ok(document);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-contrat/{contratId}")]
        public async Task<IActionResult> GetByContrat(int contratId)
        {
            var documents = await _mediator.Send(new GetDocumentsByContratRequest { ContratId = contratId });
            return Ok(documents);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] DocumentDto documentDto)
        {
            var response = await _mediator.Send(new CreateDocumentCommand { dto = documentDto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DocumentDto documentDto)
        {
            var response = await _mediator.Send(new UpdateDocumentCommand { dto = documentDto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteDocumentCommand { Id = id });
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}