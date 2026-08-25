---
name: testing
description: Creates and maintains automated tests for domain rules, application services, filtering, validation, and important MVC behavior in the real-estate project.
---

# Testing Skill

Use this skill whenever adding or modifying behavior that can reasonably be tested.

## Test Framework

Use xUnit.

## Priority

Prioritize tests for:

1. Property business rules.
2. Sale versus Rent validation.
3. Property status transitions.
4. Property filtering.
5. Application services.
6. Image-related validation.
7. Authorization-sensitive behavior where practical.

## Test Style

Tests should be:

- Focused
- Deterministic
- Independent
- Readable

Avoid testing framework behavior.

Do not create tests merely to increase coverage numbers.

## Naming

Use descriptive test names.

The name should communicate:

- Scenario
- Expected behavior

## Database Tests

Prefer unit tests for pure business rules.

Use integration tests when database behavior itself is important.

Do not make the entire test suite dependent on a developer's local SQL Server instance unless explicitly configured as an integration test environment.

## Verification

After implementation:

1. Run relevant tests.
2. Run the complete test suite when practical.
3. Fix failures before completion.