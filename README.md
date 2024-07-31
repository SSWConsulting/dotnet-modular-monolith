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

Customers:
- Can register with the website
- Must have a unique email address
- Must have an address

Orders:
- An order must be associated with a customer
- The order total must always be correct
- The order tax must always be correct
- Payment must be completed for the order to be placed (FUTURE: Consider splitting payments to it's own module)

Products:
- A customer must be able to search products
- A product can be given one or more categories

Warehouse:
- Products can be loaded into the warehouse and have their location tracked
- When an order is placed the stock level must be updated
- If there is not enough stock the order must be put on back order
- When stock is below a certain threshold the warehouse will be notified to restock
- An order can be dispatched from the warehouse

Shipping:
- Once an order is dispatched from the warehouse the shipping company must be notified
- The all 'stops' must be tracked until delivered
- The customer must be able to track their order
- Once delivered the order must be marked as complete and the customer will be notified
- Must be able to calculate the shipping cost based on time to delivery
