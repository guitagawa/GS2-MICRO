using MediatR;
using PromptAPI.Application.DTOs;

namespace PromptAPI.Application.Queries;

public record GetAllPromptsQuery : IRequest<IEnumerable<PromptDto>>;
