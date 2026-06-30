# Arquitetura e Tecnologias

## 1. Estilo arquitetural

O projeto utiliza uma organização em camadas inspirada em Clean Architecture. A separação principal é:

| Camada | Responsabilidade |
|---|---|
| Domain | Entidades centrais e regras básicas de domínio. |
| Application | Casos de uso, DTOs, contratos e regras de aplicação. |
| Infrastructure | Banco de dados, repositórios, geração de JWT e hash de senha. |
| API | Controllers, Swagger, autenticação HTTP, middleware e tela simples de login. |
| Tests | Testes automatizados dos serviços principais. |

## 2. Motivo da escolha

A arquitetura em camadas facilita manutenção, testes e evolução do sistema. A regra de negócio não fica presa diretamente ao controller nem ao banco de dados. Isso permite trocar detalhes de infraestrutura com menor impacto.

## 3. Fluxo de dependências

```txt
API -> Application -> Domain
API -> Infrastructure -> Application/Domain
Tests -> Application/Domain
```

A camada de domínio não depende das demais. A camada de aplicação define interfaces. A infraestrutura implementa essas interfaces.

## 4. Banco de dados

O projeto usa PostgreSQL em Docker. As principais tabelas são:

- `users`
- `checklists`
- `checklist_items`

O relacionamento é:

- Um usuário possui vários checklists.
- Um checklist possui vários itens.
- A exclusão de um usuário remove seus checklists.
- A exclusão de um checklist remove seus itens.

## 5. Segurança

- Senhas são armazenadas com hash.
- Login retorna JWT.
- Endpoints privados usam `[Authorize]`.
- Cada checklist possui `UserId`, impedindo acesso por outros usuários.
- A API retorna `403 Forbidden` quando o usuário tenta acessar recurso de outro usuário.

## 6. Tratamento de erros

O middleware `ExceptionHandlingMiddleware` intercepta exceções e retorna respostas padronizadas em JSON no formato `application/problem+json`.

Exemplos:

| Situação | Status |
|---|---|
| Dados inválidos | 400 |
| Login inválido | 401 |
| Acesso a checklist de outro usuário | 403 |
| Recurso não encontrado | 404 |
| E-mail duplicado | 409 |
| Erro inesperado | 500 |

## 7. Swagger

A documentação Swagger está configurada com:

- Título e descrição da API.
- Endpoints de autenticação.
- Endpoints de CRUD.
- Esquema de autenticação Bearer JWT.

## 8. Testes automatizados

Foram criados testes para validar:

- Cadastro de usuário.
- Bloqueio de e-mail duplicado.
- Bloqueio de login com senha incorreta.
- Criação de checklist.
- Bloqueio de acesso a checklist de outro usuário.
- Adição de item ao checklist.

Isso atende ao mínimo de 5 casos de teste automatizados.
