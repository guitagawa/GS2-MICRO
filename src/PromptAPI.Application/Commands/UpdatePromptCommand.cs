using MediatR;
using PromptAPI.Application.DTOs;

namespace PromptAPI.Application.Commands;

public record UpdatePromptCommand(
    Guid Id,
    string Title,
    string Content,
    string Category
) : IRequest<PromptDto>;
