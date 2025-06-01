# Ice Cream World Backend

# Summary

This is thee API for the Ice Cream World business. It provides the necessary endpoints to perform CRUD operations on ice creams records.

# Getting started

To run the app execute the following command:

```bash
dotnet run
```

# Endpoints

## GET

-   `/api/icecreams`
    -   returns all the icream flavors.
-   `/api/icecream/:id`
    -   returns the icream flavor for the specified `id`.

## POST

-   `/api/icecream`
    -   adds a new icream flavor.

## PUT

-   `/api/icecream/:id`
    -   updates the icream flavor with the specified `id`.

## DELETE

-   `/api/icecream/:id`
    -   deletes the icream flavor with the specified `id`.

# Technical Docs

-   Framework:

    -   The app was created using .NET 9 webapi template.

-   Database:

-   Containerization:

## Models

-   IceCream
