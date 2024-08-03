# dotnet-modular-monolith

[![.NET](https://github.com/danielmackay/dotnet-modular-monolith/actions/workflows/dotnet.yml/badge.svg)](https://github.com/danielmackay/dotnet-modular-monolith/actions/workflows/dotnet.yml)

## Goal

Sample repo demoing how an e-commerce system can be architected as a modular monolith using ASP.NET Core.

## Modules

### Orders Module

Responsible for Order Management

- Order Creation
- Order Lifecycle Management
- Payment: taking and recording payment
- Shipping: Calculating fee, dispatching, tracking

### Warehouse Module

Responsible for Warehouse and Inventory Management

- Stock Levels - How much do we actually have?
- Inventory Tracking - Where is the item in the warehouse?
- Supplier Chain - Where can I buy the item from?
- Restocking - When do we need to restock?
- Backorders - When do we put items on back order?
- Product Management

## Business Invariants

Warehouse:
- N/A

Product Catalog:
- A product must have a name
- A product must have a price
- A product can be given one or more categories
- A product cannot have a negative price
- A product cannot duplicate categories

Customers:
- Must have a unique email address
- Must have an address

Cart:
- Must be associated with a customer
- Must always have the correct price

Orders:
- An order must be associated with a customer
- The order total must always be correct
- The order tax must always be correct
- Shipping must be included in the total price
- Payment must be completed for the order to be placed (FUTURE: Consider splitting payments to it's own module)
