---
trigger: always_on
---

# Database and EF Core Rules

## Database

Use Microsoft SQL Server.

Use Entity Framework Core.

Use Code First.

Use EF Core Migrations.

## DbContext

Use a single application DbContext unless there is a concrete reason to split it.

The DbContext belongs to Infrastructure.

Do not inject DbContext directly into MVC Controllers.

Application services should interact with persistence through the intended application abstraction.

## Entity Configuration

Prefer Fluent API configurations for important database rules.

Keep configuration out of Domain entities when it is persistence-specific.

Use IEntityTypeConfiguration<T> classes for non-trivial entities.

## Property Data Types

Use appropriate database types.

Monetary values must use decimal.

Do not use double or float for financial values.

Area should use a numeric type appropriate for fractional square meters if required.

Year should be represented appropriately and validated.

## Money

Money fields:

- SalePrice
- Deposit
- MonthlyRent

must use decimal.

Do not perform monetary calculations using floating-point types.

## Relationships

Property has a one-to-many relationship with PropertyImage.

A PropertyImage belongs to exactly one Property.

Deleting a Property should safely handle its images and associated database records.

Physical image files must also be removed through the file-storage service when appropriate.

## Indexes

Create an index for Property.Slug.

Consider indexes for fields frequently used by public filtering and sorting.

Do not add indexes blindly.

## Migrations

Every database schema change must be represented by an EF Core migration.

Do not manually modify the database schema without updating the migration history.

Migrations must be committed to source control.

## Seed Data

Seed the single administrator account through a safe initialization mechanism.

Never store a plain-text administrator password in source code.

Development seed data must never accidentally become production credentials.

## Filtering

Filtering must be performed server-side.

Do not load all properties into memory and then filter using LINQ-to-Objects.

Build IQueryable queries and execute them in the database.

Use pagination.

Never return an unbounded list of properties from the database.

## Soft Delete

Do not introduce soft delete globally unless required.

Property lifecycle is handled through Status.

If a property must be removed permanently, use an explicit delete operation.

