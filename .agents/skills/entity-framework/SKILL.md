---
name: entity-framework
description: Handles Entity Framework Core, SQL Server, Code First models, entity configurations, queries, migrations, and database-related work for this .NET 10 real-estate project.
---

# Entity Framework Skill

Use this skill whenever working on:

- DbContext
- Entities
- EF Core configurations
- Queries
- Migrations
- SQL Server persistence
- Property filtering

## Rules

Use EF Core Code First.

Use SQL Server.

Use migrations for schema changes.

## Querying

Prefer IQueryable for composable database filtering.

Apply filters before materializing the query.

Use:

- AsNoTracking for read-only queries where appropriate.
- Select projections when a full entity is unnecessary.
- Pagination for lists.

Avoid unnecessary Include calls.

Do not load entire tables into memory.

## Filtering

Build property filters dynamically and compose them into the IQueryable.

All public and admin property filtering must execute on the database.

## Money

Use decimal for:

- SalePrice
- Deposit
- MonthlyRent

## Relationships

Property -> PropertyImages is one-to-many.

Configure relationship behavior explicitly when deletion behavior could cause unexpected results.

## Migrations

After model changes:

1. Build.
2. Create a migration.
3. Inspect the migration.
4. Apply/update the database when appropriate.
5. Verify the schema.

Never silently ignore migration errors.