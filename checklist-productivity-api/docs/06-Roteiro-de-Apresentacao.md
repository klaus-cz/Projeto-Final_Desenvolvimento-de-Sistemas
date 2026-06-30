# Roteiro de Apresentação

## Slide 1 — Título

**Checklist Productivity API**

API REST para organização de checklists, tarefas e produtividade pessoal.

Fala sugerida:

> Nosso projeto é uma API de checklist e produtividade. A ideia é permitir que um usuário crie conta, faça login e gerencie seus próprios checklists e itens de tarefa de forma segura.

## Slide 2 — Problema

Pessoas costumam organizar tarefas em notas soltas, mensagens ou planilhas. Isso dificulta controle, segurança e acompanhamento.

Fala sugerida:

> O problema que escolhemos resolver é a falta de centralização das tarefas pessoais. A API permite organizar essas informações em uma estrutura própria para checklists.

## Slide 3 — Objetivo

- Criar API REST.
- Implementar login seguro.
- Criar CRUD completo.
- Usar banco em Docker.
- Documentar com Swagger.
- Criar testes automatizados.

Fala sugerida:

> O objetivo foi cumprir os requisitos do backend: autenticação, CRUD, documentação, banco em container, tratamento de erros e testes.

## Slide 4 — Requisitos

Principais requisitos:

- Cadastro e login.
- JWT.
- CRUD de checklists.
- CRUD de itens.
- Swagger.
- Docker.
- ORM.
- Tratamento de exceções.

Fala sugerida:

> Mapeamos os requisitos funcionais e não funcionais. Os principais são cadastro, login, CRUD completo e documentação online.

## Slide 5 — Arquitetura

Camadas:

- API
- Application
- Domain
- Infrastructure
- Tests

Fala sugerida:

> Organizamos o projeto em camadas para separar responsabilidades. Controllers ficam na API, regras de aplicação ficam em Application, entidades no Domain e banco/JWT na Infrastructure.

## Slide 6 — Banco de Dados

Entidades:

- User
- Checklist
- ChecklistItem

Fala sugerida:

> O modelo de dados tem usuários, checklists e itens. Cada usuário pode ter vários checklists, e cada checklist pode ter vários itens.

## Slide 7 — Segurança

- Senha com hash.
- JWT no login.
- Rotas protegidas.
- Usuário acessa apenas seus próprios dados.

Fala sugerida:

> A segurança foi tratada com hash de senha, autenticação JWT e validação de propriedade dos recursos.

## Slide 8 — Demonstração

Ordem sugerida:

1. Subir Docker Compose.
2. Abrir Swagger.
3. Cadastrar usuário.
4. Fazer login.
5. Copiar token.
6. Autorizar Swagger com Bearer token.
7. Criar checklist.
8. Listar checklists.
9. Adicionar item.
10. Atualizar item.
11. Excluir checklist.

Fala sugerida:

> Agora vamos demonstrar a aplicação funcionando pelo Swagger, passando pelo fluxo completo desde o cadastro até o CRUD.

## Slide 9 — Testes

Testes implementados:

- Cadastro válido.
- E-mail duplicado.
- Login inválido.
- Criação de checklist.
- Bloqueio de acesso indevido.
- Adição de item.

Fala sugerida:

> Criamos testes automatizados para validar os fluxos principais e regras importantes do sistema.

## Slide 10 — Conclusão

O projeto entrega:

- Backend REST.
- Login.
- CRUD.
- Swagger.
- Docker.
- ORM.
- Tratamento de erros.
- Testes.
- Documentação.

Fala sugerida:

> Com isso, entregamos uma API funcional, documentada, testável e organizada de acordo com boas práticas de backend.
