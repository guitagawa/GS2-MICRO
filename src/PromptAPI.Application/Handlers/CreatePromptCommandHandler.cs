using MediatR;
using PromptAPI.Application.Commands;
using PromptAPI.Application.DTOs;
using PromptAPI.Domain.Entities;
using PromptAPI.Domain.Interfaces;

namespace PromptAPI.Application.Handlers;

public class CreatePromptCommandHandler : IRequestHandler<CreatePromptCommand, PromptDto>
{
    private readonly IPromptRepository _repository;

    public CreatePromptCommandHandler(IPromptRepository repository)
    {
        _repository = repository;
    }

    public async Task<PromptDto> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
    {
        var prompt = new Prompt(
            request.Title,
            request.Content,
            request.Category
        );

        await _repository.CreateAsync(prompt);

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
