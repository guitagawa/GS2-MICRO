using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PromptAPI.Domain.Entities;
using PromptAPI.Domain.Interfaces;

namespace PromptAPI.Infrastructure.Repositories;

public class PromptRepository : IPromptRepository
{
    private readonly string _connectionString;

    public PromptRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<Prompt?> GetByIdAsync(Guid id)
    {
        using var connection = CreateConnection();
        
        const string sql = @"
            SELECT Id, Title, Content, Category, CreatedAt, UpdatedAt, IsActive
            FROM Prompts
            WHERE Id = @Id";

        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });
        
        if (result == null)
            return null;

        return new Prompt(
            result.Id,
            result.Title,
            result.Content,
            result.Category,
            result.CreatedAt,
            result.UpdatedAt,
            result.IsActive
        );
    }

    public async Task<IEnumerable<Prompt>> GetAllAsync()
    {
        using var connection = CreateConnection();
        
        const string sql = @"
            SELECT Id, Title, Content, Category, CreatedAt, UpdatedAt, IsActive
            FROM Prompts
            WHERE IsActive = 1
            ORDER BY CreatedAt DESC";

        var results = await connection.QueryAsync<dynamic>(sql);
        
        return results.Select(r => new Prompt(
            r.Id,
            r.Title,
            r.Content,
            r.Category,
            r.CreatedAt,
            r.UpdatedAt,
            r.IsActive
        ));
    }

    public async Task<IEnumerable<Prompt>> GetByCategoryAsync(string category)
    {
        using var connection = CreateConnection();
        
        const string sql = @"
            SELECT Id, Title, Content, Category, CreatedAt, UpdatedAt, IsActive
            FROM Prompts
            WHERE Category = @Category AND IsActive = 1
            ORDER BY CreatedAt DESC";

        var results = await connection.QueryAsync<dynamic>(sql, new { Category = category });
        
        return results.Select(r => new Prompt(
            r.Id,
            r.Title,
            r.Content,
            r.Category,
            r.CreatedAt,
            r.UpdatedAt,
            r.IsActive
        ));
    }

    public async Task<Guid> CreateAsync(Prompt prompt)
    {
        using var connection = CreateConnection();
        
        const string sql = @"
            INSERT INTO Prompts (Id, Title, Content, Category, CreatedAt, UpdatedAt, IsActive)
            VALUES (@Id, @Title, @Content, @Category, @CreatedAt, @UpdatedAt, @IsActive)";

        await connection.ExecuteAsync(sql, new
        {
            prompt.Id,
            prompt.Title,
            prompt.Content,
            prompt.Category,
            prompt.CreatedAt,
            prompt.UpdatedAt,
            prompt.IsActive
        });

        return prompt.Id;
    }

    public async Task<bool> UpdateAsync(Prompt prompt)
    {
        using var connection = CreateConnection();
        
        const string sql = @"
            UPDATE Prompts
            SET Title = @Title,
                Content = @Content,
                Category = @Category,
                UpdatedAt = @UpdatedAt,
                IsActive = @IsActive
            WHERE Id = @Id";

        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            prompt.Id,
            prompt.Title,
            prompt.Content,
            prompt.Category,
            prompt.UpdatedAt,
            prompt.IsActive
        });

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = CreateConnection();
        
        const string sql = @"
            UPDATE Prompts
            SET IsActive = 0,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id, UpdatedAt = DateTime.UtcNow });

        return affectedRows > 0;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        using var connection = CreateConnection();
        
        const string sql = "SELECT COUNT(1) FROM Prompts WHERE Id = @Id";
        
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        
        return count > 0;
    }
}
