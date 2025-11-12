using MediatR;
using PromptAPI.Application.DTOs;

namespace PromptAPI.Application.Commands;

public record CreatePromptCommand(
    string Title,
    string Content,
    string Category
) : IRequest<PromptDto>;
