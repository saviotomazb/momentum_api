# Momentum API

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-4169E1?style=for-the-badge&logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=for-the-badge&logo=docker)
![JWT](https://img.shields.io/badge/Auth-JWT-000000?style=for-the-badge&logo=jsonwebtokens)

> API REST desenvolvida com ASP.NET Core para gerenciamento de usuários, autenticação e hábitos. O projeto utiliza Clean Architecture, Entity Framework Core, PostgreSQL, JWT Authentication e Docker para execução local.

## Pré-requisitos

Antes de começar, verifique se você tem instalado:

- [.NET SDK 10.0](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/) com Docker Compose
- Um cliente HTTP, como Swagger, Postman, Insomnia ou REST Client

## Instalando o Momentum API

Clone o repositório e acesse a pasta do projeto:

```bash
git clone <url-do-repositorio>
cd momentum_api
```

Restaure as dependências:

```bash
dotnet restore
```

Configure as variáveis de ambiente no arquivo `.env.development`:

```env
ASPNETCORE_ENVIRONMENT=Development

POSTGRES_DB=momentum_db
POSTGRES_USER=momentum_user
POSTGRES_PASSWORD=momentum_password

DB_HOST=localhost
DB_PORT=5432

JWT_SECRET=sua-chave-secreta-com-pelo-menos-32-caracteres
JWT_ISSUER=MomentumAPI
JWT_AUDIENCE=MomentumClient
JWT_EXPIRATION_MINUTES=60
```

Suba o banco de dados PostgreSQL:

```bash
docker compose up -d
```

Aplique as migrations:

```bash
dotnet ef database update --project src/Momentum.Infrastructure --startup-project src/Momentum.API
```

## Usando o Momentum API

Execute a API:

```bash
dotnet run --project src/Momentum.API
```

Por padrão, a aplicação fica disponível em:

- HTTP: `http://localhost:5271`
- HTTPS: `https://localhost:7168`
- Swagger: `https://localhost:7168/swagger`

### Endpoints disponíveis

#### Registrar usuário

```http
POST /api/auth/register
Content-Type: application/json
```

```json
{
  "name": "Savio",
  "email": "savio@example.com",
  "password": "senha-segura"
}
```

Resposta:

```json
{
  "token": "<jwt>"
}
```

#### Login

```http
POST /api/auth/login
Content-Type: application/json
```

```json
{
  "email": "savio@example.com",
  "password": "senha-segura"
}
```

Resposta:

```json
{
  "token": "<jwt>"
}
```

Para acessar endpoints protegidos, envie o token no cabeçalho:

```http
Authorization: Bearer <jwt>
```

## Estrutura do projeto

```text
momentum_api/
+-- src/
|   +-- Momentum.API/              # Controllers, configuração HTTP, Swagger e autenticação
|   +-- Momentum.Application/      # Casos de uso, DTOs, interfaces e serviços
|   +-- Momentum.Domain/           # Entidades, enums e regras de domínio
|   +-- Momentum.Infrastructure/   # Banco de dados, EF Core, migrations e repositórios
+-- tests/
|   +-- Momentum.UnitTests/        # Testes unitários
+-- docker-compose.yml             # PostgreSQL local
+-- momentum_api.slnx              # Solução do projeto
```

## Executando testes

```bash
dotnet test
```

## Tecnologias utilizadas

### Backend

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- BCrypt

### Arquitetura e Qualidade

- Clean Architecture
- Repository Pattern
- Dependency Injection

### Testes

- xUnit
- Moq
- FluentAssertions

### Infraestrutura

- Docker
- Docker Compose
- Swagger

## Arquitetura

O projeto segue os princípios da Clean Architecture, organizado nas seguintes camadas:

- API: controllers, autenticação e configuração HTTP
- Application: casos de uso, DTOs e contratos
- Domain: entidades e regras de negócio
- Infrastructure: persistência, Entity Framework Core e integrações externas

A comunicação entre camadas é realizada por abstrações, mantendo baixo acoplamento e alta testabilidade.

## Contribuindo

Para contribuir com o Momentum API:

1. Faça um fork deste repositório.
2. Crie um branch: `git checkout -b minha-feature`.
3. Faça suas alterações e confirme-as: `git commit -m "Adiciona minha feature"`.
4. Envie para o branch remoto: `git push origin minha-feature`.
5. Abra uma pull request.

Como alternativa, consulte a documentação do GitHub em [como criar uma solicitacao pull](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).

## Colaboradores

Agradecemos as seguintes pessoas que contribuiram para este projeto:

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/saviotomazb" title="Perfil de Sávio Tomaz no GitHub">
        <img src="https://avatars.githubusercontent.com/saviotomazb" width="100px;" alt="Foto de Sávio Tomaz no GitHub"/><br>
        <sub>
          <b>Sávio Tomaz</b>
        </sub>
      </a>
    </td>
  </tr>
</table>
