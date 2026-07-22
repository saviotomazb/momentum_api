# Momentum API

Backend da aplicação **Momentum**, desenvolvido com **ASP.NET Core**, responsável pelas regras de negócio, autenticação, persistência de dados e disponibilização dos serviços consumidos pela aplicação **Momentum Web**.

---

## 📖 Sobre o Projeto

O **Momentum API** é a API REST do ecossistema **Momentum**.

Seu objetivo é centralizar toda a lógica de negócio da aplicação, disponibilizando endpoints seguros para autenticação de usuários, gerenciamento de hábitos, tarefas, finanças e demais funcionalidades do sistema.

A aplicação foi desenvolvida utilizando **ASP.NET Core** e seguindo os princípios da **Clean Architecture**, com foco em escalabilidade, organização e facilidade de manutenção.

---

## ✨ Funcionalidades

### ✅ Implementadas

* 🔐 Autenticação e autorização com JWT
* 👤 Cadastro e gerenciamento de usuários
* ✅ Gerenciamento de hábitos
* 📈 Acompanhamento da evolução do usuário
* 🌐 API REST
* 📄 Documentação automática com Swagger/OpenAPI
* 🗄 Persistência de dados com PostgreSQL

### 🚧 Em desenvolvimento

* 📋 Gerenciamento de tarefas
* 💰 Gerenciamento financeiro

---

## 🛠️ Stack Tecnológica

### Framework

* ASP.NET Core (.NET 10)

### Linguagem

* C#

### Persistência de Dados

* Entity Framework Core
* PostgreSQL
* Npgsql Entity Framework Provider

### Segurança

* JWT Bearer Authentication
* BCrypt.Net

### Validação

* FluentValidation

### Documentação

* Swagger (Swashbuckle OpenAPI)

### Logging

* Serilog

### Configuração

* DotNetEnv

### Arquitetura

* Clean Architecture
* Repository Pattern
* Dependency Injection

### Testes

* xUnit
* Moq
* FluentAssertions
* Coverlet

---

## 📁 Estrutura do Projeto

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

> A solução foi organizada seguindo os princípios da **Clean Architecture**, separando responsabilidades entre as camadas de apresentação, aplicação, domínio e infraestrutura.

---

## 🚀 Como Executar

### Pré-requisitos

* .NET SDK 10
* Docker
* Docker Compose

### Clonar o repositório

```bash
git clone https://github.com/saviotomazb/momentum_api.git

cd momentum_api
```

### Restaurar as dependências

```bash
dotnet restore
```

### Configurar as variáveis de ambiente

Edite o arquivo:

```text
.env.development
```

Configure as credenciais do PostgreSQL e os parâmetros de autenticação JWT.

### Subir o banco de dados

```bash
docker compose up -d
```

### Aplicar as migrations

```bash
dotnet ef database update --project src/Momentum.Infrastructure --startup-project src/Momentum.API
```

### Executar a aplicação

```bash
dotnet run --project src/Momentum.API
```

Por padrão, a API estará disponível em:

```text
https://localhost:7168
```

Swagger:

```text
https://localhost:7168/swagger
```

---

## 🔗 Projeto Relacionado

- **[Momentum Web](https://github.com/saviotomazb/momentum_web.git)** — Interface web responsável pela experiência do usuário e consumo desta API.

---

## 🤝 Contribuindo

Contribuições são sempre bem-vindas.

Caso encontre algum problema ou tenha sugestões de melhoria, fique à vontade para abrir uma **Issue** ou enviar uma **Pull Request**, siga estas etapas:

1. Bifurque este repositório.
2. Crie um branch: `git checkout -b minha-feature`.
3. Faça suas alterações e confirme-as: `git commit -m "Minha feature"`.
4. Envie para o branch remoto: `git push origin minha-feature`.
5. Abra uma pull request.

Antes de contribuir, certifique-se de:

* Manter o código padronizado.
* Escrever código legível e reutilizável.
* Executar os testes antes de enviar alterações.