---
trigger: always_on
---

# ASP.NET Core MVC Rules

## MVC

Use ASP.NET Core MVC with Razor Views.

Do not introduce SPA frameworks.

Controllers must remain thin.

Business logic belongs in Application services.

## Controllers

Controllers should:

1. Validate the request.
2. Call the appropriate application service.
3. Handle the result.
4. Return the appropriate View or Redirect.

Do not put complex database queries or business rules inside controllers.

Use conventional MVC routing where practical.

## Routing

Public property pages must use SEO-friendly routes.

Preferred structure:

/properties
/properties/{slug}

The slug identifies the property publicly.

The numeric database ID must not be required in the public URL.

Admin routes should be clearly separated.

Preferred structure:

/admin
/admin/properties
/admin/properties/create
/admin/properties/edit/{id}

## Razor Views

Keep Razor Views focused on presentation.

Do not place business logic inside Views.

Use strongly typed ViewModels.

Use partial views for genuinely reusable UI components.

Do not create excessive partials for tiny fragments.

## Tag Helpers

Prefer built-in Tag Helpers where useful.

Use standard ASP.NET Core MVC conventions.

## Forms

Forms must include:

- Proper labels
- Validation messages
- Anti-forgery protection
- Accessible controls
- Correct input types

## Anti-Forgery

All state-changing POST requests must use anti-forgery protection.

## Persian and RTL

The HTML document must use RTL direction.

Use appropriate Persian typography and spacing.

Avoid hard-coded left/right CSS when logical CSS properties can be used.

Prefer:

- margin-inline
- padding-inline
- inset-inline

over directional properties where practical.

## Bootstrap

Use Bootstrap for layout and components.

Do not mix multiple UI frameworks.

Keep custom CSS limited to project-specific styling.