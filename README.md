# 🎬 Estrutura do Projeto

**Solução:** `MovieRentals.sln`

| Camada/Pasta   | Projeto            | Tipo    | Descrição resumida                                                   |
| -------------- | ------------------ | ------- | ------------------------------------------------------------------- |
| Web            | `Catalog.Api`      | WebAPI  | API de Catálogo; expõe endpoints CRUD e usa Oracle via EF Core.     |
| Web            | `Rentals.Api`      | WebAPI  | API de Locação de filmes com integração ao Oracle + consumo de DTOs.|
| Web            | `WebApp.Mvc`       | MVC     | Front-end MVC que consome as APIs e exibe informações ao usuário.   |
| Core           | `Core.Application` | Library | Casos de uso/serviços de aplicação; depende de Domain.              |
| Core           | `Core.Domain`      | Library | Entidades, interfaces e regras de domínio.                          |
| Infrastructure | `Core.Infrastructure` | Library | EF Core + **Migrations** + `Oracle.EntityFrameworkCore`.            |

> Todos os projetos compilam e executam localmente via `dotnet run`.  
> **Swagger** habilitado nas duas WebAPIs em `/swagger`.

---

# 🧪 Requisitos Técnicos (atendidos)

* **Swagger** em todas as WebAPIs (`Swashbuckle.AspNetCore`).
* **Pelo menos 3 princípios SOLID** (descritos abaixo).
* **Clean Code** (padrões de nomeação, classes coesas e baixo acoplamento).
* **2 WebAPIs + 1 MVC + 3+ libraries**.
* **Migrations** preparadas no projeto `Core.Infrastructure` com instruções abaixo.

---

# 🔌 Integrações Obrigatórias

## WebAPI com Oracle (EF Core)

* **Projeto de Startup:** `Catalog.Api`
* **Infra de Persistência:** `Core.Infrastructure` (referencia `Oracle.EntityFrameworkCore`).
* **Connection String:** definir em `appsettings.Development.json`, por exemplo:

```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=rmXXXXXX;Password=SuaSenha;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))(CONNECT_DATA=(SID=ORCL)));"
  }
}
```

---

# ⚙️ Como Rodar Localmente (Passo a passo)

## Pré-requisitos

* **.NET SDK 8.0+**
* **Oracle** acessível (FIAP ou local) e credenciais válidas.
* **Ferramenta** `dotnet-ef` instalada globalmente.

## Clonar e restaurar

```bash
git clone <url-do-seu-repo>
cd MovieRentals
dotnet restore
```

## Aplicar Migrations no Oracle

```bash
dotnet ef database update   -p src/Libraries/Core.Infrastructure/Core.Infrastructure.csproj   -s src/Services/Rentals/Rentals.Api/Rentals.Api.csproj
```

## Subir os serviços (exemplo de portas)

```bash
# API de Catálogo
dotnet run --project src/Services/Catalog/Catalog.Api/Catalog.Api.csproj --urls http://localhost:5001

# API de Locação
dotnet run --project src/Services/Rentals/Rentals.Api/Rentals.Api.csproj --urls http://localhost:5003

# MVC
dotnet run --project src/Web/WebApp.Mvc/WebApp.Mvc.csproj --urls http://localhost:5005
```

## Acessos rápidos

* **Swagger Catalog:** [http://localhost:5001/swagger](http://localhost:5001/swagger)  
* **Swagger Rentals:** [http://localhost:5003/swagger](http://localhost:5003/swagger)  
* **MVC:** [http://localhost:5005/](http://localhost:5005/)  

---

# 📚 Endpoints Principais (exemplos)

## Catalog.Api (Oracle/EF Core)

* `GET /api/movies` — lista de filmes
* `GET /api/movies/{id}` — filme por id
* `POST /api/movies` — cria filme

## Rentals.Api (Oracle/EF Core)

* `POST /api/rentals` — cria uma locação
* `GET /api/rentals/{id}` — busca locação por id
* `PATCH /api/rentals/{id}/return` — devolve um filme

---

# 🧩 Aplicação de SOLID

## SRP — *Single Responsibility Principle*
* `Core.Domain`: somente entidades e interfaces.
* `Core.Application`: orquestra casos de uso.
* `Core.Infrastructure`: acesso a dados via EF Core.

## OCP — *Open/Closed Principle*
* Domínio define interfaces, infraestrutura fornece implementações.
* Possibilidade de trocar Oracle por outro banco implementando outro repositório.

## DIP — *Dependency Inversion Principle*
* Serviços de aplicação dependem de **interfaces de domínio**, não da infraestrutura.
* Inversão feita por **DI** no startup das APIs.

---

# 🧼 Clean Code (práticas adotadas)

* Nomeação clara e coesa.
* Baixo acoplamento e alta coesão.
* DTOs para evitar exposição de entidades do domínio.
* Configurações externas (`appsettings.json`) para connection strings e URLs.

---

# 🧭 System Design (Mermaid)

```mermaid
flowchart LR
  subgraph Client
    Browser[Usuário (Browser)]
  end

  subgraph Web
    MVC["WebApp.Mvc (MVC)"]
    API_Catalog["Catalog.Api (WebAPI)"]
    API_Rentals["Rentals.Api (WebAPI)"]
  end

  subgraph Core
    App["Core.Application"]
    Domain["Core.Domain"]
  end

  subgraph Infrastructure
    Persist["Core.Infrastructure (EF Core + Migrations)"]
  end

  Browser --> MVC
  MVC --> API_Catalog
  MVC --> API_Rentals

  API_Catalog --> App
  App --> Domain
  API_Catalog --> Persist

  API_Rentals --> App
  API_Rentals --> Persist
```

---

# ✅ Checklist de Conformidade

* [x] **2 WebAPIs** (`Catalog.Api`, `Rentals.Api`)
* [x] **1 MVC** (`WebApp.Mvc`)
* [x] **3+ Libraries** (`Core.Domain`, `Core.Application`, `Core.Infrastructure`)
* [x] **Swagger** nas WebAPIs (`/swagger`)
* [x] **Oracle via EF Core** (Catalog + Rentals)
* [x] **Library com Migrations** (`Core.Infrastructure`)
* [x] **SOLID** (SRP, OCP, DIP) descritos
* [x] **Clean Code** adotado
* [x] **System Design** documentado (Mermaid)
