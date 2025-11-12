using MediatR;
using PromptAPI.Application.Commands;
using PromptAPI.Domain.Interfaces;

namespace PromptAPI.Application.Handlers;

public class DeletePromptCommandHandler : IRequestHandler<DeletePromptCommand, bool>
{
    private readonly IPromptRepository _repository;

    public DeletePromptCommandHandler(IPromptRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeletePromptCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsAsync(request.Id);
        
        if (!exists)
            throw new KeyNotFoundException($"Prompt with ID {request.Id} not found.");

        return await _repository.DeleteAsync(request.Id);
    }
}
