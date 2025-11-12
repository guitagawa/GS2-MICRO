using MediatR;
using PromptAPI.Application.DTOs;

namespace PromptAPI.Application.Queries;

public record GetPromptByIdQuery(Guid Id) : IRequest<PromptDto?>;
