# Diagramas do Projeto

Os diagramas abaixo usam Mermaid, formato renderizado automaticamente pelo GitHub em arquivos Markdown.

## 1. Diagrama de casos de uso

```mermaid
flowchart LR
    Visitante[Usuário não autenticado]
    Usuario[Usuário autenticado]

    UC1((Cadastrar usuário))
    UC2((Fazer login))
    UC3((Criar checklist))
    UC4((Listar checklists))
    UC5((Consultar checklist))
    UC6((Atualizar checklist))
    UC7((Excluir checklist))
    UC8((Adicionar item))
    UC9((Atualizar item))
    UC10((Excluir item))

    Visitante --> UC1
    Visitante --> UC2
    Usuario --> UC3
    Usuario --> UC4
    Usuario --> UC5
    Usuario --> UC6
    Usuario --> UC7
    Usuario --> UC8
    Usuario --> UC9
    Usuario --> UC10
```

## 2. Diagrama de classes

```mermaid
classDiagram
    class User {
        +Guid Id
        +string Name
        +string Email
        +string PasswordHash
        +DateTime CreatedAt
    }

    class Checklist {
        +Guid Id
        +Guid UserId
        +string Title
        +string Description
        +DateTime DueDate
        +bool IsArchived
        +DateTime CreatedAt
        +DateTime UpdatedAt
        +Update()
        +AddItem()
        +RemoveItem()
    }

    class ChecklistItem {
        +Guid Id
        +Guid ChecklistId
        +string Title
        +bool IsCompleted
        +int Position
        +DateTime CreatedAt
        +DateTime CompletedAt
        +Update()
        +SetCompletion()
    }

    class AuthService {
        +RegisterAsync()
        +LoginAsync()
    }

    class ChecklistService {
        +GetAllAsync()
        +GetByIdAsync()
        +CreateAsync()
        +UpdateAsync()
        +DeleteAsync()
        +AddItemAsync()
        +UpdateItemAsync()
        +DeleteItemAsync()
    }

    User "1" --> "many" Checklist
    Checklist "1" --> "many" ChecklistItem
    AuthService --> User
    ChecklistService --> Checklist
```

## 3. Diagrama entidade-relacionamento

```mermaid
erDiagram
    USERS ||--o{ CHECKLISTS : owns
    CHECKLISTS ||--o{ CHECKLIST_ITEMS : contains

    USERS {
        uuid id PK
        string name
        string email UK
        string password_hash
        datetime created_at
    }

    CHECKLISTS {
        uuid id PK
        uuid user_id FK
        string title
        string description
        datetime due_date
        boolean is_archived
        datetime created_at
        datetime updated_at
    }

    CHECKLIST_ITEMS {
        uuid id PK
        uuid checklist_id FK
        string title
        boolean is_completed
        int position
        datetime created_at
        datetime completed_at
    }
```

## 4. Diagrama de sequência — Login

```mermaid
sequenceDiagram
    actor U as Usuário
    participant F as Tela de Login
    participant A as AuthController
    participant S as AuthService
    participant R as UserRepository
    participant P as PasswordHasher
    participant T as TokenService

    U->>F: Informa e-mail e senha
    F->>A: POST /api/auth/login
    A->>S: LoginAsync(request)
    S->>R: GetByEmailAsync(email)
    R-->>S: User
    S->>P: Verify(password, hash)
    P-->>S: true
    S->>T: Generate(user)
    T-->>S: JWT
    S-->>A: AuthResponse
    A-->>F: 200 OK + token
    F-->>U: Exibe token/resposta
```

## 5. Diagrama de sequência — Criar checklist

```mermaid
sequenceDiagram
    actor U as Usuário autenticado
    participant C as ChecklistsController
    participant S as ChecklistService
    participant R as ChecklistRepository
    participant D as PostgreSQL

    U->>C: POST /api/checklists + Bearer token
    C->>C: Obtém UserId do token
    C->>S: CreateAsync(userId, request)
    S->>S: Valida título
    S->>R: AddAsync(checklist)
    R->>D: INSERT checklists
    D-->>R: OK
    R-->>S: Checklist salvo
    S-->>C: ChecklistResponse
    C-->>U: 201 Created
```
