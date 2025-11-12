using MediatR;

namespace PromptAPI.Application.Commands;

public record DeletePromptCommand(Guid Id) : IRequest<bool>;
