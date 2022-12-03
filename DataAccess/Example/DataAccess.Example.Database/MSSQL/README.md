# Microsoft SQL Server

## Docker instructions

Start by running the docker file to create a MS-SQL server container;

cd to \MSSQL\ms-sql-docker and run;

```powershell
docker-compose -f docker-compose.database.yaml up
```

The yaml file defines the password for the SA account as "Passw0rd!?2223", if you want to change this you'll need to edit the "SA_PASSWORD" element.

Once MS-SQL server is up and running run the docker file to create a Liquibase container and run an update command;

cd to \MSSQL\liquibase-docker and run;

```
docker-compose -f docker-compose.liquibase-update.yaml up
```

## Files

### ms-sql-docker

This folder is the root for the MS SQL docker set up, it contains the docker-compose file for bringing up the MS SQL server instance

### data

This folder contains the files required to initialise the MS SQL server instance with a blank database named "DataAccessExampleDatabase", Liquibase will require this to run update commands.

The files are as follows;

- DockerFile - builds the docker image and run entrypoint.sh
- entrypoint.sh - copies setup.sql to the docker image and runs it against the MS SQL server
- setup.sql - SQL script to create the "DataAccessExampleDatabase" database

*notes*; 

- You might need to delete the container and image for MS SQL if changes are made to the DockerFile
- Be careful modifying the entrypoint.sh file - docker is very picky about EOL - you need to make sure they are Linux EOL see https://stackoverflow.com/a/66786043
- If you want to change the password used by MS SQL server you'll need to modify;
  - DockerFile
  - entrypoint.sh
  - liquibase.properties file (found in the liquibase-docker\config folder)