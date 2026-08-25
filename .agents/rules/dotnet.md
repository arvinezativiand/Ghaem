---
trigger: always_on
---

# .NET 10 Coding Standards

## Target Framework

The project targets .NET 10.

Use current stable .NET 10 APIs and conventions.

Do not use obsolete APIs when a modern equivalent exists.

## Nullable Reference Types

Nullable reference types must be enabled.

Treat nullable warnings seriously.

Do not silence nullable warnings with the null-forgiving operator unless there is a clear and justified reason.

## Async

Use async/await for I/O operations.

Database calls must use asynchronous EF Core methods when an async equivalent exists.

Examples:

- ToListAsync
- FirstOrDefaultAsync
- SingleOrDefaultAsync
- SaveChangesAsync
- FindAsync

Do not wrap synchronous I/O in Task.Run.

## Dependency Injection

Use the built-in ASP.NET Core dependency injection container.

Register services using appropriate lifetimes.

Default to scoped lifetime for application services that depend on DbContext.

Avoid service locator patterns.

Do not inject IServiceProvider into application code unless there is a strong reason.

## Configuration

Use strongly typed options when configuration contains multiple related settings.

Do not hard-code:

- Connection strings
- Credentials
- File storage paths that should be configurable
- Security-sensitive configuration

Use appsettings.json and environment-specific configuration appropriately.

Never commit secrets.

## Logging

Use ILogger<T>.

Log useful contextual information.

Do not log:

- Passwords
- Authentication cookies
- Sensitive user data
- Uploaded file contents

Avoid excessive logging of normal successful requests.

## Validation

Use server-side validation as the source of truth.

Client-side validation can improve UX but must never replace server-side validation.

Business validation must not depend solely on HTML or JavaScript.

## Code Quality

Prefer readable code over clever code.

Keep methods focused.

Avoid very large classes.

Avoid premature abstractions.

Do not duplicate substantial business logic.

Do not optimize prematurely.
