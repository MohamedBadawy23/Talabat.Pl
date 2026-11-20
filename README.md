
# Talabat.Pl – E-Commerce REST API

A RESTful E-Commerce API built with ASP.NET Core 6, designed to manage product catalog data, customer shopping baskets, and related business entities.

The project demonstrates a layered architecture and several backend development patterns, including the Generic Repository Pattern, Specification Pattern, Entity Framework Core, Redis caching, and ASP.NET Core Identity.

## Key Features

### Product Catalog
- Retrieve products with pagination.
- Filter products by brand and type.
- Search products by name.
- Sort products by name or price.
- Retrieve product details by ID.
- Retrieve available product brands and types.
- Map product entities to DTOs using AutoMapper.
- Resolve product image URLs.

### Shopping Basket
- Create or update customer baskets.
- Retrieve baskets by customer ID.
- Delete customer baskets.
- Store basket data in Redis.
- Configure basket expiration after one day.

### Employee Management
- Retrieve employees.
- Retrieve employee details.
- Include related department information.
- Map employee entities to DTOs.

### Data Management
- SQL Server database integration.
- Entity Framework Core migrations.
- Fluent API entity configurations.
- Initial data seeding from JSON files.

### Error Handling
- Custom API error response models.
- Model validation error handling.
- Custom exception middleware implementation.

## Technologies & Tools

| Technology | Purpose |
|---|---|
| C# | Backend development |
| .NET 6 | Application framework |
| ASP.NET Core Web API | REST API development |
| Entity Framework Core 6 | ORM and database access |
| SQL Server | Relational database |
| Redis | Shopping basket storage |
| ASP.NET Core Identity | Identity management setup |
| AutoMapper | Object-to-object mapping |
| Swagger / OpenAPI | API documentation |
| Generic Repository Pattern | Data access abstraction |
| Specification Pattern | Reusable query filtering |
| LINQ | Data querying |

## Architecture

The solution is organized into separate projects:

```text
Talabat.Pl/
│
├── Talabat.Pl/
│   ├── Controllers/
│   ├── DTO/
│   ├── Errors/
│   ├── Extensions/
│   ├── Helper/
│   ├── MiddleWares/
│   └── Program.cs
│
├── Talabat.Core/
│   ├── Entities/
│   ├── Repositories/
│   └── Specefication/
│
├── Talabat.Repository/
│   ├── Data/
│   │   ├── Configuration/
│   │   ├── Context/
│   │   ├── DataSeeding/
│   │   └── Migrations/
│   ├── Identity/
│   ├── Repositories/
│   └── Specefication/
│
├── Talabat.Services/
│
└── Talabat.Pl.sln
```

### Project Responsibilities

- **Talabat.Pl:** API presentation layer, controllers, DTOs, dependency registration, and application configuration.
- **Talabat.Core:** Domain entities, repository interfaces, and specification definitions.
- **Talabat.Repository:** EF Core DbContexts, database configurations, migrations, Redis integration, and repository implementations.
- **Talabat.Services:** Separate project currently included in the solution.

## API Endpoints

The following endpoints are based on the controllers currently implemented in the repository.

### Products

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/product` | Get paginated products |
| GET | `/api/product/{id}` | Get product by ID |
| GET | `/api/product/Brands` | Get all brands |
| GET | `/api/product/Types` | Get all product types |

Supported product query parameters:

- `PageIndex`
- `PageSize`
- `Search`
- `BrandId`
- `TypeId`
- `sort`

Supported sorting values:
- `PriceAsc`
- `PriceDesc`

Example:

```http
GET /api/product?PageIndex=1&BrandId=1&sort=PriceAsc
```

### Shopping Basket

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/basket/{id}` | Retrieve customer basket |
| POST | `/api/basket` | Create or update basket |
| DELETE | `/api/basket?id={id}` | Delete customer basket |

### Employees

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/employee` | Get employees |
| GET | `/api/employee/id?id={id}` | Get employee by ID |

## Getting Started

### Prerequisites

Make sure you have the following installed:

- .NET 6 SDK
- SQL Server
- Redis Server
- Visual Studio or Visual Studio Code

### 1. Clone the Repository

```bash
git clone https://github.com/MohamedBadawy23/Talabat.Pl.git
```

### 2. Navigate to the Project

```bash
cd Talabat.Pl
```

### 3. Configure Connection Strings

Update `Talabat.Pl/appsettings.json` with your local database and Redis settings.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION",
    "IdenetityConnection": "YOUR_IDENTITY_DATABASE_CONNECTION",
    "RedisConnection": "localhost"
  }
}
```

Replace the placeholders with your own connection settings.

### 4. Run Redis

Make sure your Redis server is running and accessible through the configured connection string.

### 5. Run the Application

```bash
dotnet restore
dotnet build
dotnet run --project Talabat.Pl
```

### 6. Explore the API

Open the Swagger URL displayed by the application to explore and test the available endpoints.

## Database & Caching

The project uses two storage technologies:

- **SQL Server:** Stores products, brands, types, employees, departments, and Identity data.
- **Redis:** Stores customer shopping baskets as serialized JSON values with a one-day expiration.

Database migrations and initial product catalog seeding are triggered during application startup.

## Design Patterns

- **Generic Repository Pattern:** Provides reusable data access operations.
- **Specification Pattern:** Encapsulates filtering, sorting, pagination, and related entity inclusion.
- **Dependency Injection:** Registers repositories and application dependencies.
- **DTO Pattern:** Separates API response models from domain entities.

## Future Improvements

- Implement complete customer authentication and authorization endpoints.
- Add order creation and order tracking.
- Add checkout and payment integration.
- Introduce a dedicated application/service layer.
- Add automated unit and integration tests.
- Improve validation and API documentation.
- Strengthen basket ownership and access control.

## Author

**Mohamed Badawy**

GitHub: [MohamedBadawy23](https://github.com/MohamedBadawy23)

Repository: [Talabat.Pl](https://github.com/MohamedBadawy23/Talabat.Pl)
