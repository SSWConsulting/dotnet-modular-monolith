# Architectural Decision Records

## ADR1 - Ensure Related Data is Created When Seeding the DB

### Context

In normal execution of the application DB updates will cause domain events to fire.  These domain events can have side affects that cause other DB updates.

There are several pieces of complicated infrastructure that make this work.

We need to ensure that related data is updated when seeding the DB via the following:

- `WarehouseDbContextInitializer`
- `CatalogDbContextInitializer`

### Decision

Instead of emulating the domain event propagating infrastructure, we will instead pass the data between initializers.  This means a bit more manual work, but is a much simpler solution.

If this turns out to be too complex in future we will need to revist this.
