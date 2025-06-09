# Ice Cream World Back-end

# Summary

This is the API for the Ice Cream World business. It provides the necessary endpoints to perform CRUD operations on ice creams records.

# Getting started

To run the app execute the following command:

```bash
dotnet run
```

## Run backend with PostgreSQL DB

```docker
docker compose up --build -d
```

# Technical Docs

-   Framework:

    -   .NET 9 webapi

-   Database:

    -   Postgredb

-   Containerization:

    -   Docker

-   [Front-end repository](https://github.com/lionelherrerobattista/ice_cream_world_frontend)

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

## Models

-   IceCream
-   Serving container
