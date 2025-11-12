using MediatR;
using PromptAPI.Application.DTOs;
using PromptAPI.Application.Queries;
using PromptAPI.Domain.Interfaces;

namespace PromptAPI.Application.Handlers;

public class GetPromptsByCategoryQueryHandler : IRequestHandler<GetPromptsByCategoryQuery, IEnumerable<PromptDto>>
{
    private readonly IPromptRepository _repository;

    public GetPromptsByCategoryQueryHandler(IPromptRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PromptDto>> Handle(GetPromptsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var prompts = await _repository.GetByCategoryAsync(request.Category);

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
