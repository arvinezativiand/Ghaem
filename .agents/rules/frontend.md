---
trigger: always_on
---

# Frontend and UI Rules

## Framework

Use:

- Razor Views
- Bootstrap
- Custom CSS only when necessary
- Vanilla JavaScript only when necessary

Do not introduce React, Vue, Angular, or another frontend framework.

## Design Direction

The website is Persian and RTL.

The visual style should feel like a modern, professional real-estate website.

Prioritize:

- Clear property cards
- Strong property photography
- Readable prices
- Easy filtering
- Mobile responsiveness
- Clear call-to-action elements

Do not over-design the site.

## Property Cards

Property cards should show useful information at a glance.

At minimum:

- Cover image
- Title
- Property type
- Transaction type
- Area
- Bedrooms
- Price information
- Location
- Status where appropriate

## Property Details

The property detail page should contain:

- Image gallery
- Title
- Description
- Main property information
- Price information
- Amenities
- Address
- Construction year
- Property type
- Transaction type
- Status
- Contact information

## Filters

The public Properties page must support comprehensive filtering.

Filtering should include relevant fields such as:

- Transaction type
- Property type
- Minimum/maximum price
- Minimum/maximum deposit
- Minimum/maximum monthly rent
- Minimum/maximum area
- Bedroom count
- Parking
- Storage
- Elevator
- Floor
- Construction year
- Status where appropriate

The admin property list should provide equivalent management-oriented filtering.

## Pagination

Property lists must use pagination.

Do not render hundreds of property cards on a single page.

## Accessibility

Use:

- Semantic HTML
- Proper labels
- Keyboard-accessible controls
- Meaningful alt text for property images
- Visible validation messages
- Appropriate heading hierarchy
