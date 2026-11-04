# StockPilot

A small inventory management REST API built with **ASP.NET Core Minimal APIs**, **Entity Framework Core** and **SQLite**. It manages products, categories and suppliers, with filtering, sorting, search and full CRUD.

## Features

- CRUD for products, categories and suppliers
- Product list with filtering (category, max stock), sorting (name, price, stock) and ascending/descending order
- Product search by name, using a parameterized SQL query (safe against SQL injection)
- Relational model: every product belongs to one category and one supplier
- Input validation with clear error responses (400 / 404 / 409)
- Referential integrity: categories and suppliers that are still in use cannot be deleted (409 Conflict)
- "Delete all" endpoints for resetting data
- Automatic database creation, migration and seeding on startup (26 sample products, 4 categories, 5 suppliers)
- Response projection, so the API returns category and supplier names instead of nested entities

## Tech stack

- C# / ASP.NET Core Minimal APIs
- Entity Framework Core (Code First, Migrations)
- SQLite
- Swagger / OpenAPI

## Getting started

Prerequisite: the .NET SDK matching the version in `StockPilot.csproj`.

```bash
git clone https://github.com/LSchumacher32/stockpilot-api.git
cd stockPilot-api
dotnet run
```

On first start the app creates `stockpilot.db`, applies the migrations and seeds the sample data. The URL is printed in the console. Open `/swagger` on that URL to try the endpoints.

To reset the data, stop the app and delete `stockpilot.db`. It is recreated on the next start.

## API overview

### Products

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/products` | List products. Optional query: `category`, `maxStock`, `sort` (`price`, `stock`), `dir` (`desc`) |
| GET | `/products/search?name=` | Search products by name |
| GET | `/products/{id}` | Get one product |
| POST | `/products` | Create a product |
| PUT | `/products/{id}` | Update a product |
| DELETE | `/products/{id}` | Delete a product |
| DELETE | `/products` | Delete all products |

### Categories

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/categories` | List categories |
| GET | `/categories/{id}` | Get one category |
| POST | `/categories` | Create a category |
| PUT | `/categories/{id}` | Rename a category |
| DELETE | `/categories/{id}` | Delete a category (409 if in use) |
| DELETE | `/categories` | Delete all categories (409 if products exist) |

### Suppliers

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/suppliers` | List suppliers |
| GET | `/suppliers/{id}` | Get one supplier |
| POST | `/suppliers` | Create a supplier |
| PUT | `/suppliers/{id}` | Update a supplier |
| DELETE | `/suppliers/{id}` | Delete a supplier (409 if in use) |
| DELETE | `/suppliers` | Delete all suppliers (409 if products exist) |

## Examples

Low-stock electronics, cheapest first:

```
GET /products?category=Electronics&maxStock=10&sort=price
```

Create a product:

```json
POST /products
{
  "productName": "Wireless Charger",
  "categoryId": 1,
  "supplierId": 1,
  "price": 19.90,
  "stockLevel": 40
}
```

Example response from `GET /products/search?name=maus`:

```json
[
  {
    "id": 1,
    "productName": "Bluetooth Maus",
    "price": 24.9,
    "stockLevel": 8,
    "categoryId": 1,
    "supplierId": 1,
    "category": "Electronics",
    "supplier": "TechNova GmbH"
  }
]
```

## Project structure

```
Data/         DbContext and database seeder
Endpoints/    Minimal API endpoint groups (products, lookups)
Models/       Entities and DTOs
Migrations/   EF Core migrations
Program.cs    App setup, migration and seeding on startup
```

## Notes

- To delete everything, remove products first, then categories and suppliers. Otherwise the API returns 409.
- The "delete all" endpoints are unprotected. They are meant for local use and demos.
