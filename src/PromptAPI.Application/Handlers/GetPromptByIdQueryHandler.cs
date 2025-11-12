using MediatR;
using PromptAPI.Application.DTOs;
using PromptAPI.Application.Queries;
using PromptAPI.Domain.Interfaces;

namespace PromptAPI.Application.Handlers;

public class GetPromptByIdQueryHandler : IRequestHandler<GetPromptByIdQuery, PromptDto?>
{
    private readonly IPromptRepository _repository;

    public GetPromptByIdQueryHandler(IPromptRepository repository)
    {
        _repository = repository;
    }

    public async Task<PromptDto?> Handle(GetPromptByIdQuery request, CancellationToken cancellationToken)
    {
        var prompt = await _repository.GetByIdAsync(request.Id);

        if (prompt == null)
            return null;

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
