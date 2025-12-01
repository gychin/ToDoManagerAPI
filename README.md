# ToDoManagerAPI

A simple RESTful API for managing to-do items, built with ASP.NET Core (.NET 9) and Entity Framework Core.

## Features

- CRUD operations for to-do items
- SQLite database integration
- CORS support for frontend development (default: http://localhost:3000)
- Swagger UI for API documentation (enabled in development)
- Modern .NET 9 minimal hosting model

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQLite](https://www.sqlite.org/download.html) (optional, database file is auto-created)
- Visual Studio 2022 or later

### Setup

1. **Clone the repository:**
2. The database connection gets overwritten in the ToDoManagerAPI.Data folder in the ToDoDbContext.OnConfiguring() method
3. The database is created in: C:\Users\{Username}\AppData\Local\LocalDatabase.db
4. **Run database migrations:**
- In Visual Studio 2022, Select the View Menu and select Terminal to open the Developer PowerShell window and type these two commands:
- dotnet ef migrations add InitialCreate
- dotnet ef database update

### API Endpoints

| Method | Endpoint                | Description                |
|--------|------------------------ |---------------------------|
| GET    | `/api/ToDoItems`        | List all to-do items      |
| GET    | `/api/ToDoItems/{id}`   | Get a specific item       |
| POST   | `/api/ToDoItems`        | Create a new item         |
| PUT    | `/api/ToDoItems/{id}`   | Update an item            |
| DELETE | `/api/ToDoItems/{id}`   | Delete an item            |

### Example ToDoItem Model
-I assume you are familiar with using Swagger

## Development

- Swagger UI is available at `/swagger` in development mode.
- In Visual Studio start debug or start without debug and a terminal window will open to indicate listening is on: https://localhost:2001 or http://localhost:2000
- In a browser navigate to: https://localhost:2001/swagger/index.html
- CORS is enabled for `http://localhost:3000` by default (see `Program.cs`). Which will be used by the react front end.

## Contributing

Please follow the coding standards and guidelines defined in `.editorconfig` and `CONTRIBUTING.md`.

## License

This project is licensed under the MIT License.
