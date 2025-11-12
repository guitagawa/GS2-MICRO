using MediatR;
using PromptAPI.Application.Commands;
using PromptAPI.Application.DTOs;
using PromptAPI.Domain.Interfaces;

namespace PromptAPI.Application.Handlers;

public class UpdatePromptCommandHandler : IRequestHandler<UpdatePromptCommand, PromptDto>
{
    private readonly IPromptRepository _repository;

    public UpdatePromptCommandHandler(IPromptRepository repository)
    {
        _repository = repository;
    }

    public async Task<PromptDto> Handle(UpdatePromptCommand request, CancellationToken cancellationToken)
    {
        var prompt = await _repository.GetByIdAsync(request.Id);
        
        if (prompt == null)
            throw new KeyNotFoundException($"Prompt with ID {request.Id} not found.");

        prompt.Update(request.Title, request.Content, request.Category);
        
        await _repository.UpdateAsync(prompt);

        return new PromptDto(
            prompt.Id,
            prompt.Title,
            prompt.Content,
            prompt.Category,
            prompt.CreatedAt,
            prompt.UpdatedAt,
            prompt.IsActive
        );
    }
}
