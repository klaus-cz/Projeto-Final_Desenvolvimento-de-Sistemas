# Checklist Productivity API

API REST para gerenciamento de checklists e produtividade pessoal, com autenticação JWT, CRUD completo de checklists e itens, documentação Swagger, tratamento centralizado de exceções, testes automatizados e banco PostgreSQL em container Docker.

## Tema escolhido

**API de Checklist e Produtividade**.

A aplicação permite que usuários criem uma conta, façam login e gerenciem seus próprios checklists, tarefas e itens de produtividade.

## Tecnologias utilizadas

- C# + ASP.NET Core Web API
- Entity Framework Core como ORM
- PostgreSQL em container Docker
- JWT Bearer Authentication
- Swagger/OpenAPI
- xUnit para testes automatizados
- Arquitetura em camadas inspirada em Clean Architecture

## Estrutura do projeto

```txt
ChecklistProductivityApi/
├── src/
│   ├── ChecklistProductivity.Api/              # Controllers, Program, Middleware, tela de login
│   ├── ChecklistProductivity.Application/      # DTOs, interfaces, serviços e regras de aplicação
│   ├── ChecklistProductivity.Domain/           # Entidades e regras centrais do domínio
│   └── ChecklistProductivity.Infrastructure/   # EF Core, PostgreSQL, JWT, hashing e repositórios
├── tests/
│   └── ChecklistProductivity.Tests/            # Testes automatizados
├── docs/                                      # Documentação acadêmica do projeto
├── docker-compose.yml
├── Dockerfile
└── ChecklistProductivityApi.sln
```

## Funcionalidades

### Autenticação

- Cadastro de usuário
- Login com geração de token JWT
- Senhas armazenadas com hash seguro
- Rotas protegidas por autenticação

### Checklist

- Criar checklist
- Listar checklists do usuário autenticado
- Consultar checklist por ID
- Atualizar checklist
- Excluir checklist

### Itens do checklist

- Adicionar item a um checklist
- Atualizar item
- Marcar item como concluído ou pendente
- Excluir item

### Extras técnicos

- Swagger documentado
- Middleware global de erro
- Banco PostgreSQL via Docker
- ORM com Entity Framework Core
- Pelo menos 5 testes automatizados
- Separação por camadas

## Como executar com Docker

Na raiz do projeto, execute:

```bash
docker compose up --build
```

A API ficará disponível em:

```txt
http://localhost:8080
```

Swagger:

```txt
http://localhost:8080/swagger
```

Tela simples de login:

```txt
http://localhost:8080/login.html
```

## Como executar localmente sem Docker

Pré-requisitos:

- .NET SDK instalado
- PostgreSQL rodando localmente

Configure a connection string no arquivo:

```txt
src/ChecklistProductivity.Api/appsettings.Development.json
```

Depois execute:

```bash
dotnet restore
dotnet run --project src/ChecklistProductivity.Api
```

## Como rodar os testes

```bash
dotnet test
```

## Principais endpoints

### Autenticação

| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/auth/register` | Cadastra usuário |
| POST | `/api/auth/login` | Realiza login e retorna JWT |

### Checklists

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/checklists` | Lista checklists do usuário logado |
| GET | `/api/checklists/{id}` | Consulta checklist por ID |
| POST | `/api/checklists` | Cria checklist |
| PUT | `/api/checklists/{id}` | Atualiza checklist |
| DELETE | `/api/checklists/{id}` | Exclui checklist |

### Itens

| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/checklists/{checklistId}/items` | Adiciona item |
| PUT | `/api/checklists/{checklistId}/items/{itemId}` | Atualiza item |
| DELETE | `/api/checklists/{checklistId}/items/{itemId}` | Exclui item |

## Exemplo de uso

### Cadastro

```bash
curl -X POST http://localhost:8080/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"name":"Daniel","email":"daniel@email.com","password":"Senha@12345"}'
```

### Login

```bash
curl -X POST http://localhost:8080/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"daniel@email.com","password":"Senha@12345"}'
```

Copie o token retornado e use no header:

```txt
Authorization: Bearer SEU_TOKEN
```

## Observação sobre banco de dados

Para facilitar a avaliação acadêmica, a API usa `Database.EnsureCreated()` na inicialização. Em ambiente real, o ideal seria utilizar migrations versionadas do Entity Framework Core.
