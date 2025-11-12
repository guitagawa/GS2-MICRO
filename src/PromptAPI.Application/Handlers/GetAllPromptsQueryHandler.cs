using MediatR;
using PromptAPI.Application.DTOs;
using PromptAPI.Application.Queries;
using PromptAPI.Domain.Interfaces;

namespace PromptAPI.Application.Handlers;

public class GetAllPromptsQueryHandler : IRequestHandler<GetAllPromptsQuery, IEnumerable<PromptDto>>
{
    private readonly IPromptRepository _repository;

    public GetAllPromptsQueryHandler(IPromptRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PromptDto>> Handle(GetAllPromptsQuery request, CancellationToken cancellationToken)
    {
        var prompts = await _repository.GetAllAsync();

        return prompts.Select(p => new PromptDto(
            p.Id,
            p.Title,
            p.Content,
            p.Category,
            p.CreatedAt,
            p.UpdatedAt,
            p.IsActive
        ));
    }
}
