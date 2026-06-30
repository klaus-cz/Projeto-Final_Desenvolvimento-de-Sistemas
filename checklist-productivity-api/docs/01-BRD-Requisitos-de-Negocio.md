# BRD — Documento de Requisitos de Negócio

## 1. Visão geral

O projeto consiste em uma API REST de checklist e produtividade. O sistema permite que usuários criem uma conta, façam login e gerenciem checklists pessoais com itens, prazos e status de conclusão.

## 2. Problema de negócio

Muitas pessoas organizam tarefas em anotações soltas, mensagens ou planilhas. Isso dificulta acompanhamento, priorização e controle do que já foi concluído. A API resolve esse problema centralizando checklists e itens em uma aplicação organizada, segura e acessível por um front-end.

## 3. Objetivos de negócio

- Permitir que cada usuário gerencie seus próprios checklists.
- Proteger dados pessoais por autenticação.
- Facilitar integração com front-end web ou mobile.
- Fornecer documentação online via Swagger.
- Demonstrar boas práticas de backend, arquitetura, testes e persistência.

## 4. Público-alvo

- Estudantes que precisam organizar atividades.
- Profissionais que controlam tarefas diárias.
- Usuários que querem acompanhar metas e rotinas.
- Times pequenos que futuramente poderiam compartilhar checklists.

## 5. Escopo do projeto

### Dentro do escopo

- Cadastro de usuário.
- Login com token JWT.
- CRUD completo de checklists.
- CRUD de itens dentro de checklists.
- Banco de dados em container Docker.
- Swagger documentado.
- Tratamento de erros.
- Testes automatizados.
- Tela simples de login para validação.

### Fora do escopo inicial

- Compartilhamento de checklists entre usuários.
- Notificações por e-mail ou push.
- Recuperação de senha.
- Aplicativo mobile.
- Dashboard analítico avançado.

## 6. Regras de negócio

| Código | Regra |
|---|---|
| RN01 | Cada usuário deve ter e-mail único. |
| RN02 | A senha não pode ser armazenada em texto puro. |
| RN03 | Somente usuários autenticados podem acessar checklists. |
| RN04 | Um usuário só pode visualizar, alterar ou excluir seus próprios checklists. |
| RN05 | Um checklist deve possuir título obrigatório. |
| RN06 | Um checklist pode conter zero ou mais itens. |
| RN07 | Um item deve possuir título obrigatório. |
| RN08 | Um item pode ser marcado como concluído ou pendente. |
| RN09 | A exclusão de checklist remove também seus itens. |
| RN10 | Erros devem retornar mensagens padronizadas e status HTTP adequado. |

## 7. Indicadores de sucesso

- Usuário consegue cadastrar e logar com sucesso.
- Usuário consegue executar CRUD completo de checklists.
- Usuário consegue executar CRUD de itens.
- Swagger permite testar os endpoints.
- Testes automatizados executam com sucesso.
- Banco sobe por Docker Compose.
