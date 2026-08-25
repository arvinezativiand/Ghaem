---
trigger: always_on
---

# Architecture Rules

## Architecture Style

Use a lightweight layered architecture inspired by Onion Architecture.

The solution must contain:

- RealEstate.Domain
- RealEstate.Application
- RealEstate.Infrastructure
- RealEstate.Web
- RealEstate.Tests

## Dependency Direction

The dependency direction must be:

Domain <- Application <- Infrastructure
                         ^
                         |
                        Web

More precisely:

- Domain must not depend on any other project.
- Application may depend only on Domain.
- Infrastructure may depend on Application and Domain.
- Web may depend on Application and Infrastructure.
- Tests may reference the projects required for testing.

Never create circular dependencies.

## Domain Layer

The Domain layer contains:

- Entities
- Enums
- Domain-level business rules
- Value objects only when they provide real value

Do not put:

- EF Core configuration
- MVC controllers
- Razor ViewModels
- HTTP concerns
- File-system concerns
- SQL queries

inside Domain.

## Application Layer

The Application layer contains application use cases.

Examples:

- CreateProperty
- UpdateProperty
- DeleteProperty
- GetProperty
- GetProperties
- ChangePropertyStatus
- UploadPropertyImages
- DeletePropertyImage

Use clear application services.

Do not introduce MediatR or CQRS.

Application services may use interfaces for infrastructure concerns.

## Infrastructure Layer

Infrastructure contains:

- EF Core DbContext
- EF Core configurations
- Migrations
- Database implementation
- File storage implementation
- Authentication infrastructure where appropriate
- External infrastructure concerns

EF Core entity configurations should use separate IEntityTypeConfiguration classes when the configuration is substantial.

## Web Layer

The Web project is the ASP.NET Core MVC application.

It contains:

- Controllers
- ViewModels
- Views
- Filters
- Model binding concerns
- Web-specific validation
- Authentication configuration
- Static assets
- wwwroot

Controllers must remain thin.

Controllers should coordinate requests and application services rather than contain business logic.

## ViewModels

Do not expose domain entities directly to Razor Views when a dedicated ViewModel is more appropriate.

Use separate ViewModels for:

- Property list
- Property details
- Create property
- Edit property
- Property filters
- Login
- Dashboard

Avoid creating ViewModels for trivial cases where they provide no meaningful benefit.

## Services

Prefer focused services.

Examples:

- IPropertyService
- IPropertyImageService
- IFileStorageService

Do not create abstractions merely because a pattern says so.

Every abstraction must solve an actual problem.

## Error Handling

Use centralized error handling.

The application must:

- Log unexpected exceptions.
- Show user-friendly error pages.
- Never expose stack traces in production.
- Never expose database exceptions directly to users.

Validation errors should be shown next to the relevant form fields.

## Naming

Use clear, conventional C# naming.

- PascalCase for classes, methods, properties, enums.
- camelCase for local variables and parameters.
- Interfaces begin with I.
- Async methods end with Async.
- Use meaningful names instead of abbreviations.