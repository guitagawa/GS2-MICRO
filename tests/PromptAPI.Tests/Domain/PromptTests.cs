using PromptAPI.Domain.Entities;
using Xunit;

namespace PromptAPI.Tests.Domain;

public class PromptTests
{
    [Fact]
    public void Prompt_Constructor_ShouldCreateValidPrompt()
    {
        // Arrange
        var title = "Test Prompt";
        var content = "This is a test prompt content";
        var category = "Testing";

        // Act
        var prompt = new Prompt(title, content, category);

        // Assert
        Assert.NotEqual(Guid.Empty, prompt.Id);
        Assert.Equal(title, prompt.Title);
        Assert.Equal(content, prompt.Content);
        Assert.Equal(category, prompt.Category);
        Assert.True(prompt.IsActive);
        Assert.Null(prompt.UpdatedAt);
        Assert.True(prompt.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Prompt_Update_ShouldUpdateProperties()
    {
        // Arrange
        var prompt = new Prompt("Original Title", "Original Content", "Original Category");
        var newTitle = "Updated Title";
        var newContent = "Updated Content";
        var newCategory = "Updated Category";

        // Act
        prompt.Update(newTitle, newContent, newCategory);

        // Assert
        Assert.Equal(newTitle, prompt.Title);
        Assert.Equal(newContent, prompt.Content);
        Assert.Equal(newCategory, prompt.Category);
        Assert.NotNull(prompt.UpdatedAt);
    }

    [Fact]
    public void Prompt_Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var prompt = new Prompt("Test", "Content", "Category");

        // Act
        prompt.Deactivate();

        // Assert
        Assert.False(prompt.IsActive);
        Assert.NotNull(prompt.UpdatedAt);
    }

    [Fact]
    public void Prompt_Activate_ShouldSetIsActiveToTrue()
    {
        // Arrange
        var prompt = new Prompt("Test", "Content", "Category");
        prompt.Deactivate();

        // Act
        prompt.Activate();

        // Assert
        Assert.True(prompt.IsActive);
        Assert.NotNull(prompt.UpdatedAt);
    }

    [Fact]
    public void Prompt_ReconstructionConstructor_ShouldCreatePromptWithAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var title = "Test";
        var content = "Content";
        var category = "Category";
        var createdAt = DateTime.UtcNow.AddDays(-1);
        var updatedAt = DateTime.UtcNow;
        var isActive = true;

        // Act
        var prompt = new Prompt(id, title, content, category, createdAt, updatedAt, isActive);

        // Assert
        Assert.Equal(id, prompt.Id);
        Assert.Equal(title, prompt.Title);
        Assert.Equal(content, prompt.Content);
        Assert.Equal(category, prompt.Category);
        Assert.Equal(createdAt, prompt.CreatedAt);
        Assert.Equal(updatedAt, prompt.UpdatedAt);
        Assert.Equal(isActive, prompt.IsActive);
    }
}
