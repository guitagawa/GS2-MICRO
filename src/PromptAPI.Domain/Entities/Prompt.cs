namespace PromptAPI.Domain.Entities;

public class Prompt
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Category { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public Prompt(string title, string content, string category)
    {
        Id = Guid.NewGuid();
        Title = title;
        Content = content;
        Category = category;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    // Construtor para reconstrução do banco de dados
    public Prompt(Guid id, string title, string content, string category, DateTime createdAt, DateTime? updatedAt, bool isActive)
    {
        Id = id;
        Title = title;
        Content = content;
        Category = category;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        IsActive = isActive;
    }

    public void Update(string title, string content, string category)
    {
        Title = title;
        Content = content;
        Category = category;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
