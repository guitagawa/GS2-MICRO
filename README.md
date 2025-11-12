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


