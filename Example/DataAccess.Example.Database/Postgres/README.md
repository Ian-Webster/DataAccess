# Postgres

## Docker instructions

Start by running the docker file to create a Postgres server container;

Open a command prompt, cd to this folder and run;

```powershell
docker-compose -f docker-compose.database.yaml up
```

Once the Postgres server is up and running run the docker file to create a Liquibase container and run an update command;

In the same folder open another command prompt and run;

```
docker-compose -f docker-compose.liquibase-update.yaml up
```

## Files

In the root of the Postgres folder we have the two docker compose yaml files we need to create the Postgres database server and Liquibase service containers.

We also have init.sql, this is used in the docker-compose.database.yaml file as an entry point given to Postgres, it creates the "DataAccessExampleDatabase" for us.

### config

This folder contains the files required by Liquibase to run change scripts on the "DataAccessExampleDatabase" database

The files are as follows;

- changelog.xml - bootstrapping for Liquibase
- liquibase.properties - Liquibase configuration file (passwords etc)

### scripts

This folder contains the change scripts Liquibase will run against the "DataAccessExampleDatabase" database.