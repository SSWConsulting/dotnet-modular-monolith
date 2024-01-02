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
