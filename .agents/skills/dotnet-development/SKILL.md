---
name: dotnet-development
description: Provides project-specific .NET 10 coding guidance for implementing and modifying backend, application, domain, and MVC functionality in this real-estate project.
---

# .NET Development Skill

Use this skill when implementing or modifying .NET 10 application code.

## Goals

Produce simple, maintainable, idiomatic C#.

Follow the project Rules before implementing code.

## Process

1. Inspect the existing solution structure.
2. Identify which layer owns the requested behavior.
3. Reuse existing services and abstractions.
4. Avoid introducing unnecessary patterns.
5. Implement the smallest clean change.
6. Compile the affected projects.
7. Run relevant tests.
8. Fix compilation and test failures before considering the task complete.

## Layer Selection

Use:

- Domain for business concepts and domain rules.
- Application for use cases and application orchestration.
- Infrastructure for persistence and external resources.
- Web for HTTP, MVC, Views, ViewModels, and presentation.

## Before Adding a New Abstraction

Ask:

- Does the abstraction hide a real infrastructure boundary?
- Does it improve testability?
- Does it simplify the application?

If the answer is no, do not add it.

## Async

Use asynchronous APIs for database and file I/O.

Pass CancellationToken where the existing architecture supports it.

## Verification

After implementation:

- Build the solution.
- Run tests.
- Inspect warnings.
- Verify the changed behavior.

Never claim completion without verification.