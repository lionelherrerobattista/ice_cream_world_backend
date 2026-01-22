CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250607171407_InitialCreate') THEN
    CREATE TABLE "ServingContainers" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Price" double precision,
        "Photo" text,
        CONSTRAINT "PK_ServingContainers" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250607171407_InitialCreate') THEN
    CREATE TABLE "Flavors" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Description" text,
        "Photo" text,
        "ServingContainerId" uuid,
        CONSTRAINT "PK_Flavors" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Flavors_ServingContainers_ServingContainerId" FOREIGN KEY ("ServingContainerId") REFERENCES "ServingContainers" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250607171407_InitialCreate') THEN
    CREATE INDEX "IX_Flavors_ServingContainerId" ON "Flavors" ("ServingContainerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250607171407_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250607171407_InitialCreate', '9.0.5');
    END IF;
END $EF$;
COMMIT;

