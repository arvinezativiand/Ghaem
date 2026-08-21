---
trigger: always_on
---

# Project Overview

## Purpose

This project is a small real-estate agency website.

The system has two main areas:

1. Public website for visitors.
2. Private admin area for the agency owner.

The agency owner can create, edit, publish, hide, and manage property listings.

Visitors can browse properties, filter them, and open a dedicated detail page for each property.

The project is intentionally small and must remain simple, maintainable, and understandable.

## Technology Stack

- .NET 10
- ASP.NET Core MVC
- Razor Views
- Bootstrap
- SQL Server
- Entity Framework Core
- Code First
- EF Core Migrations
- Cookie-based authentication
- xUnit for automated tests
- Local server file storage for property images

## Language and UI

The entire public website and admin panel are Persian.

The website must use RTL layout.

All user-facing text should be Persian unless there is a strong technical reason otherwise.

Use Persian-friendly formatting for prices and numbers where appropriate.

## Main Domain Concept

The main domain entity is Property.

A Property contains:

- Id
- Title
- Description
- Area
- BedroomCount
- HasParking
- HasStorage
- Floor
- Unit
- TotalFloors
- SalePrice
- Deposit
- MonthlyRent
- HasElevator
- ConstructionYear
- PropertyType
- TransactionType
- Address
- Slug
- Status
- CreatedAt
- UpdatedAt
- Images

## Transaction Types

A property can have exactly one transaction type:

- Sale
- Rent

For Sale properties:

- SalePrice is required.
- Deposit and MonthlyRent must not be required.

For Rent properties:

- Deposit and MonthlyRent are required.
- SalePrice must not be required.

The application must enforce these rules both in server-side validation and in the domain/application logic.

## Property Types

The initial property types are:

- Apartment
- House
- Villa
- Shop
- Office
- Land
- Other

The implementation should make it easy to add new property types later.

## Property Statuses

A property can have exactly one status:

- Available
- Sold
- Rented
- Hidden

Meaning:

- Available: publicly visible and currently available.
- Sold: property has been sold.
- Rented: property has been rented.
- Hidden: property exists in the system but is not publicly visible.

Do not delete properties merely because they are sold or rented.

## Public Website

The public website must include:

- Home
- Properties
- Property Details
- About
- Contact

The Properties page must support comprehensive filtering.

The Property Details page must have a stable, shareable URL.

Example:

/properties/modern-120-meter-apartment

Opening the URL must directly display that property's details.

## Admin Area

The admin area must include:

- Login
- Dashboard
- Property List
- Create Property
- Edit Property
- Delete Property
- Change Property Status

The admin property list must support the same comprehensive filtering capabilities as the public property list where appropriate.

Only the single administrator can access the admin area.

## Images

Property images are stored on the local server.

Images must not be stored as binary data inside SQL Server.

Each Property can have multiple images.

The system should support:

- Uploading multiple images
- Removing images
- Selecting a primary/cover image
- Ordering images

Image uploads must be validated by extension, MIME type, and reasonable file-size limits.

Uploaded filenames must never be trusted.

Generate safe unique filenames.

## SEO

SEO is important.

Property pages must have:

- SEO-friendly title
- Meta description
- Canonical URL
- Open Graph metadata where appropriate
- SEO-friendly slug
- Clean readable URLs

Property URLs must be directly shareable.

Do not expose database implementation details in public URLs.

## Scope Restrictions

Do not introduce unnecessary complexity.

Do NOT add:

- Microservices
- CQRS
- MediatR
- Event sourcing
- Message brokers
- Redis
- Generic Repository
- Complex Unit of Work
- Domain events unless genuinely required
- Unnecessary abstractions
- Frontend SPA frameworks
- React
- Vue
- Angular

The project is a small MVC application and should stay a small MVC application.