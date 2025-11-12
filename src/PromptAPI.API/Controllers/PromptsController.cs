using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromptAPI.Application.Commands;
using PromptAPI.Application.DTOs;
using PromptAPI.Application.Queries;

namespace PromptAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromptsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PromptsController> _logger;

    public PromptsController(IMediator mediator, ILogger<PromptsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtém todos os prompts ativos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PromptDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PromptDto>>> GetAll()
    {
        _logger.LogInformation("Getting all prompts");
        var query = new GetAllPromptsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Obtém um prompt por ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PromptDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting prompt with ID: {Id}", id);
        var query = new GetPromptByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Prompt with ID {Id} not found", id);
            return NotFound(new { message = $"Prompt with ID {id} not found" });
        }

        return Ok(result);
    }

    /// <summary>
    /// Obtém prompts por categoria
    /// </summary>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<PromptDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PromptDto>>> GetByCategory(string category)
    {
        _logger.LogInformation("Getting prompts by category: {Category}", category);
        var query = new GetPromptsByCategoryQuery(category);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Cria um novo prompt
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PromptDto>> Create([FromBody] CreatePromptDto dto)
    {
        _logger.LogInformation("Creating new prompt with title: {Title}", dto.Title);
        var command = new CreatePromptCommand(dto.Title, dto.Content, dto.Category);
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Atualiza um prompt existente
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PromptDto>> Update(Guid id, [FromBody] UpdatePromptDto dto)
    {
        _logger.LogInformation("Updating prompt with ID: {Id}", id);
        var command = new UpdatePromptCommand(id, dto.Title, dto.Content, dto.Category);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    /// <summary>
    /// Exclui um prompt (soft delete)
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting prompt with ID: {Id}", id);
        var command = new DeletePromptCommand(id);
        await _mediator.Send(command);
        
        return NoContent();
    }
}
