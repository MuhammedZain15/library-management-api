# Library Management API

A production-minded REST API for managing books, authors, publishers, and library users. The project demonstrates clean ASP.NET Core fundamentals, relational data modelling, secure cookie authentication, validation, and asynchronous data access.

## Highlights

- RESTful CRUD endpoints for books, authors, and publishers
- Search by book title/description and author/publisher name
- ASP.NET Core Identity registration, login, logout, and current-user endpoints
- Protected write operations with public read access
- Many-to-many book/author relationship and publisher association
- Zero-configuration SQLite persistence
- Service layer, request models, validation, seed data, OpenAPI, and health check

## Tech stack

- .NET 10 and ASP.NET Core Web API
- Entity Framework Core 10
- SQLite
- ASP.NET Core Identity
- OpenAPI and Swagger UI

## Project structure

```text
LibraryApp/
├── Controllers/      HTTP endpoints and authentication
├── Data/             DbContext and database seeding
├── Model/            Domain and Identity entities
├── Services/         Business logic and data access
└── View_Model/       Validated API request models
```

## Run locally

Prerequisite: .NET 10 SDK. No database server or extra setup is required.

### Windows: one-click start

Clone or download the repository, then double-click `run.cmd` in the repository root. It restores the application, starts it over HTTP, and opens Swagger automatically.

### Terminal

```bash
git clone https://github.com/MuhammedZain15/library-management-api.git
cd library-management-api/LibraryApp
dotnet restore
dotnet run
```

The application creates a local `library.db` file and seeds sample books, authors, and publishers automatically on first startup.

Open the Swagger URL printed in the terminal, or check `GET /health`.

> If Visual Studio asks for a startup project, right-click the `LibraryApp` web project and choose **Set as Startup Project**.

## Troubleshooting

- **Couldn't find a project to run:** run `run.cmd`, or pass `--project LibraryApp/LibraryApp.csproj` from the repository root.
- **Database file error:** ensure the project folder is writable. During development, deleting `LibraryApp/library.db` recreates a fresh database on the next run.
- **Port already in use:** stop the process using port `5181`, or change the `http` URL in `Properties/launchSettings.json`.

## Main endpoints

| Method | Route | Access | Purpose |
|---|---|---|---|
| POST | `/api/auth/register` | Public | Create an account and sign in |
| POST | `/api/auth/login` | Public | Sign in with email, username, or phone |
| POST | `/api/auth/logout` | Authenticated | End the current session |
| GET | `/api/auth/me` | Authenticated | Return the signed-in user |
| GET/POST | `/api/books` | Public / Authenticated | List/search or create books |
| GET/PUT/DELETE | `/api/books/{id}` | Public / Authenticated | Read, update, or remove a book |
| GET/POST | `/api/authors` | Public / Authenticated | List/search or create authors |
| GET/PUT/DELETE | `/api/authors/{id}` | Public / Authenticated | Author operations |
| GET/POST | `/api/publishers` | Public / Authenticated | List/search or create publishers |
| GET/PUT/DELETE | `/api/publishers/{id}` | Public / Authenticated | Publisher operations |

Authenticated calls use the Identity application cookie returned by register/login.

## Example book request

```json
{
  "title": "Domain-Driven Design",
  "price": 450,
  "description": "A practical guide to modelling complex software domains.",
  "author": "Eric Evans",
  "isRead": false,
  "rate": 5,
  "genre": "Software Engineering",
  "publisherId": 1,
  "authorsId": [1]
}
```

Additional ready-to-run examples are in `LibraryApp.http`.

## Portfolio talking points

- Designing and validating REST resources
- Modelling one-to-many and many-to-many relationships
- Separating controllers from business/data access logic
- Securing state-changing operations with ASP.NET Identity
- Handling missing resources and invalid relationships safely

## Future improvements

- Pagination and structured filtering
- Role-based authorization for librarians and administrators
- Docker Compose environment with SQL Server

## License

This project is available for educational and portfolio use.
