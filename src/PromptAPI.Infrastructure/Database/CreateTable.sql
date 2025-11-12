-- Criação da tabela Prompts
CREATE TABLE Prompts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Índices para melhor performance
CREATE INDEX IX_Prompts_Category ON Prompts(Category);
CREATE INDEX IX_Prompts_IsActive ON Prompts(IsActive);
CREATE INDEX IX_Prompts_CreatedAt ON Prompts(CreatedAt DESC);
