using PromptAPI.Domain.Entities;

namespace PromptAPI.Domain.Interfaces;

public interface IPromptRepository
{
    Task<Prompt?> GetByIdAsync(Guid id);
    Task<IEnumerable<Prompt>> GetAllAsync();
    Task<IEnumerable<Prompt>> GetByCategoryAsync(string category);
    Task<Guid> CreateAsync(Prompt prompt);
    Task<bool> UpdateAsync(Prompt prompt);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
