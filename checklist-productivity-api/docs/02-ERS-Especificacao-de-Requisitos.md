# ERS — Especificação de Requisitos de Software

## 1. Requisitos funcionais

| Código | Requisito | Prioridade |
|---|---|---|
| RF01 | O sistema deve permitir cadastro de usuário com nome, e-mail e senha. | Alta |
| RF02 | O sistema deve permitir login com e-mail e senha. | Alta |
| RF03 | O sistema deve gerar token JWT após login válido. | Alta |
| RF04 | O sistema deve proteger as rotas de checklist com autenticação. | Alta |
| RF05 | O sistema deve permitir criar checklist. | Alta |
| RF06 | O sistema deve permitir listar checklists do usuário autenticado. | Alta |
| RF07 | O sistema deve permitir consultar checklist por ID. | Alta |
| RF08 | O sistema deve permitir atualizar checklist. | Alta |
| RF09 | O sistema deve permitir excluir checklist. | Alta |
| RF10 | O sistema deve permitir adicionar itens a um checklist. | Alta |
| RF11 | O sistema deve permitir atualizar itens de checklist. | Alta |
| RF12 | O sistema deve permitir excluir itens de checklist. | Alta |
| RF13 | O sistema deve expor documentação Swagger/OpenAPI. | Média |
| RF14 | O sistema deve possuir tela simples de login para demonstração. | Média |

## 2. Requisitos não funcionais

| Código | Requisito | Descrição |
|---|---|---|
| RNF01 | Segurança | Senhas devem ser salvas com hash e rotas privadas devem exigir JWT. |
| RNF02 | Persistência | Dados devem ser armazenados em banco PostgreSQL executado em Docker. |
| RNF03 | Manutenibilidade | Código separado em camadas e organizado por responsabilidade. |
| RNF04 | Testabilidade | Serviços principais devem possuir testes automatizados. |
| RNF05 | Documentação | A API deve possuir Swagger e README com instruções de uso. |
| RNF06 | Tratamento de erros | Exceções devem retornar JSON padronizado com status HTTP correto. |
| RNF07 | Portabilidade | Projeto deve ser executável via Docker Compose. |
| RNF08 | Clean Code | Nomes claros, métodos pequenos e separação de responsabilidades. |

## 3. Atores

| Ator | Descrição |
|---|---|
| Usuário não autenticado | Pode se cadastrar e fazer login. |
| Usuário autenticado | Pode gerenciar seus próprios checklists e itens. |
| Avaliador/professor | Pode testar a API via Swagger e verificar código, documentação e testes. |

## 4. Casos de uso resumidos

| Caso de uso | Ator | Fluxo principal |
|---|---|---|
| Cadastrar usuário | Usuário não autenticado | Envia nome, e-mail e senha; sistema cria usuário e retorna token. |
| Fazer login | Usuário não autenticado | Envia e-mail e senha; sistema valida credenciais e retorna token. |
| Criar checklist | Usuário autenticado | Envia título, descrição e prazo; sistema cria checklist. |
| Listar checklists | Usuário autenticado | Solicita lista; sistema retorna apenas dados do próprio usuário. |
| Atualizar checklist | Usuário autenticado | Envia novos dados; sistema valida propriedade e atualiza. |
| Excluir checklist | Usuário autenticado | Solicita exclusão; sistema remove checklist e itens. |
| Gerenciar itens | Usuário autenticado | Adiciona, atualiza, conclui ou exclui itens. |

## 5. Modelo de dados

### User

- Id
- Name
- Email
- PasswordHash
- CreatedAt

### Checklist

- Id
- UserId
- Title
- Description
- DueDate
- IsArchived
- CreatedAt
- UpdatedAt

### ChecklistItem

- Id
- ChecklistId
- Title
- IsCompleted
- Position
- CreatedAt
- CompletedAt

## 6. Critérios de aceite

- Ao cadastrar usuário com dados válidos, o sistema retorna HTTP 201.
- Ao tentar cadastrar e-mail duplicado, o sistema retorna HTTP 409.
- Ao fazer login com senha incorreta, o sistema retorna HTTP 401.
- Ao tentar acessar checklist de outro usuário, o sistema retorna HTTP 403.
- Ao criar checklist válido, o sistema retorna HTTP 201.
- Ao consultar checklist inexistente, o sistema retorna HTTP 404.
- Ao excluir checklist, ele deixa de aparecer na listagem.
- Swagger deve exibir endpoints e autenticação Bearer.
