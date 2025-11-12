using MediatR;
using PromptAPI.Application.DTOs;

namespace PromptAPI.Application.Queries;

public record GetPromptsByCategoryQuery(string Category) : IRequest<IEnumerable<PromptDto>>;
