---
name: mvc-development
description: Implements ASP.NET Core MVC controllers, ViewModels, Razor Views, routing, forms, filtering, RTL Persian UI, SEO metadata, and Bootstrap interfaces for the real-estate project.
---

# MVC Development Skill

Use this skill when working on the Web project.

## Process

1. Inspect existing controllers and ViewModels.
2. Identify the correct application service.
3. Create or modify the ViewModel.
4. Implement controller behavior.
5. Implement or update Razor Views.
6. Verify validation and authorization.
7. Verify RTL and responsive behavior.
8. Build and test.

## Controllers

Keep controllers thin.

Do not implement business rules in controllers.

Do not write complex EF queries directly inside controllers.

## ViewModels

Use dedicated ViewModels for forms and complex pages.

Do not bind domain entities directly from untrusted HTTP requests when doing so could allow over-posting.

## Routing

Public property details use:

/properties/{slug}

Admin routes use:

/admin/properties/...

## SEO

Property detail pages should dynamically generate:

- Title
- Description
- Canonical URL
- Open Graph metadata

Use the property Slug for the canonical public URL.

## Forms

Every state-changing form requires anti-forgery protection.

Create and Edit forms must display validation errors clearly.

## UI

Use Bootstrap.

Keep the interface Persian and RTL.

Use responsive layouts.