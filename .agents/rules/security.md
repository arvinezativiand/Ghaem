---
trigger: always_on
---

# Security Rules

## Authentication

There is exactly one administrator.

The admin area must require authentication.

Use secure cookie-based authentication.

Do not implement a custom authentication protocol.

## Authorization

All admin controllers/actions must require the authenticated administrator.

Public property pages must not require authentication.

Never rely on hiding admin links as an authorization mechanism.

Authorization must be enforced server-side.

## Login

The login form must:

- Validate credentials securely.
- Avoid revealing whether the username or password was incorrect.
- Use anti-forgery protection.
- Support logout.

Do not store plain-text passwords.

## Image Upload Security

Image uploads are untrusted input.

Never trust:

- Original filename
- Client-provided MIME type
- File extension

Validate uploaded files.

Generate a server-side safe filename.

Store uploads outside executable code paths where practical.

Prevent path traversal.

Do not allow uploaded files to overwrite arbitrary files.

Limit:

- File size
- Number of files per property
- Allowed image formats

Only allow supported image formats.

## Request Validation

Validate all user input on the server.

Do not trust hidden fields.

Do not trust values coming from the browser for authorization or ownership.

## XSS

Never render user-provided property descriptions as raw HTML unless the content has been explicitly sanitized.

By default, render descriptions as encoded text.

## CSRF

All state-changing requests must use anti-forgery tokens.

## SQL Injection

Use EF Core parameterized queries and LINQ.

Never concatenate user input into raw SQL.

If raw SQL is genuinely required, use parameterization.

## Secrets

Never commit:

- Passwords
- API keys
- Connection strings containing credentials
- Encryption keys

Use environment variables or appropriate secret configuration.

## Production

Production configuration must:

- Disable developer exception pages.
- Use HTTPS.
- Use secure cookies.
- Use appropriate security headers where practical.
