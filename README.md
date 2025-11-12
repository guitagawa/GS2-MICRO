# PromptAPI

API RESTful para gerenciamento de prompts desenvolvida com C#, .NET 8, seguindo os princípios de **Clean Architecture**, **CQRS** e **TDD**.

## 📋 Índice

- [Arquitetura](#arquitetura)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Branches e Etapas de Desenvolvimento](#branches-e-etapas-de-desenvolvimento)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Banco de Dados](#banco-de-dados)
- [Como Executar](#como-executar)

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, dividido em camadas com responsabilidades bem definidas:

- **Domain**: Contém as entidades de negócio e interfaces de repositórios
- **Application**: Contém a lógica de aplicação, commands, queries e handlers (CQRS)
- **Infrastructure**: Implementação de repositórios, acesso a dados com Dapper
- **API**: Camada de apresentação, controllers e configurações

### Padrões Implementados

- **CQRS (Command Query Responsibility Segregation)**: Separação entre operações de leitura e escrita
- **Repository Pattern**: Abstração do acesso a dados
- **Dependency Injection**: Inversão de controle e injeção de dependências
- **TDD (Test-Driven Development)**: Desenvolvimento orientado a testes

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0**: Framework principal
- **C# 12**: Linguagem de programação
- **Dapper**: Micro ORM para acesso a dados
- **SQL Server**: Banco de dados relacional
- **xUnit**: Framework de testes unitários
- **Git**: Controle de versão

## 📁 Estrutura do Projeto

```
PromptAPI/
├── src/
│   ├── PromptAPI.API/              # Camada de apresentação (Controllers)
│   ├── PromptAPI.Application/       # Lógica de aplicação (CQRS)
│   ├── PromptAPI.Domain/            # Entidades e interfaces
│   └── PromptAPI.Infrastructure/    # Implementação de repositórios
├── tests/
│   └── PromptAPI.Tests/            # Testes unitários e de integração
└── PromptAPI.sln                   # Solução .NET
```

## 🌿 Branches e Etapas de Desenvolvimento

### Branch: `feature/domain-modeling`

**Etapa 1: Modelagem do Domínio (3.0 pontos)**

Esta branch contém a implementação da primeira etapa do projeto:

#### Implementações Realizadas

1. **Modelagem da Classe Prompt**
   - Entidade `Prompt` com propriedades: Id, Title, Content, Category, CreatedAt, UpdatedAt, IsActive
   - Encapsulamento adequado com propriedades privadas
   - Métodos de domínio: Update(), Activate(), Deactivate()
   - Construtores para criação e reconstrução do banco de dados

2. **Interface de Repositório**
   - `IPromptRepository` definindo o contrato de acesso a dados
   - Métodos: GetByIdAsync, GetAllAsync, GetByCategoryAsync, CreateAsync, UpdateAsync, DeleteAsync, ExistsAsync

3. **Implementação do Repositório com Dapper**
   - `PromptRepository` implementando `IPromptRepository`
   - Uso de Dapper para queries SQL otimizadas
   - Gerenciamento adequado de conexões com banco de dados
   - Queries parametrizadas para prevenção de SQL Injection

4. **Configuração de Banco de Dados**
   - Connection string configurada no `appsettings.json`
   - Script SQL para criação da tabela `Prompts`
   - Índices para otimização de consultas

#### Estrutura de Dados

**Tabela: Prompts**

| Campo      | Tipo              | Descrição                          |
|------------|-------------------|------------------------------------|
| Id         | UNIQUEIDENTIFIER  | Identificador único (Primary Key)  |
| Title      | NVARCHAR(200)     | Título do prompt                   |
| Content    | NVARCHAR(MAX)     | Conteúdo do prompt                 |
| Category   | NVARCHAR(100)     | Categoria do prompt                |
| CreatedAt  | DATETIME2         | Data de criação                    |
| UpdatedAt  | DATETIME2         | Data da última atualização         |
| IsActive   | BIT               | Indica se o prompt está ativo      |

#### Pacotes NuGet Adicionados

- `Dapper` (2.1.66): Micro ORM para acesso a dados
- `Microsoft.Data.SqlClient` (6.1.3): Provider para SQL Server
- `Microsoft.Extensions.Configuration.Abstractions` (10.0.0): Abstrações de configuração

---

## ⚙️ Configuração do Ambiente

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (ou SQL Server Express)
- [Git](https://git-scm.com/)

### Clonando o Repositório

```bash
git clone <url-do-repositorio>
cd PromptAPI
```

### Restaurando Dependências

```bash
dotnet restore
```

## 🗄️ Banco de Dados

### Configuração da Connection String

Edite o arquivo `src/PromptAPI.API/appsettings.json` com suas credenciais:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PromptDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
  }
}
```

### Criação da Tabela

Execute o script SQL localizado em `src/PromptAPI.Infrastructure/Database/CreateTable.sql`:

```sql
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
```

### Testando a Conexão

A conexão com o banco de dados é estabelecida através do `PromptRepository`, que utiliza Dapper para executar queries SQL de forma eficiente e segura.

## 🚀 Como Executar

### Compilando o Projeto

```bash
dotnet build
```

### Executando os Testes

```bash
dotnet test
```

### Executando a API

```bash
cd src/PromptAPI.API
dotnet run
```

A API estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

---

## 📝 Próximas Etapas

- **Etapa 2**: Implementação do Core (Controllers, Services, Injeção de Dependência)
- **Etapa 3**: Validações e Melhorias (Tratamento de Exceções, Documentação Completa)

---

## 👤 Autor

**guitagawa**

---

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais.
