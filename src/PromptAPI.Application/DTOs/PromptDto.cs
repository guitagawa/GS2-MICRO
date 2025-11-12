namespace PromptAPI.Application.DTOs;

public record PromptDto(
    Guid Id,
    string Title,
    string Content,
    string Category,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsActive
);

public record CreatePromptDto(
    string Title,
    string Content,
    string Category
);

public record UpdatePromptDto(
    string Title,
    string Content,
    string Category
);
