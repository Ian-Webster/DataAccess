# Microsoft SQL Server

## Docker instructions

Start by running the docker file to create a MS-SQL server container;

Open a command prompt, cd to this folder and run;

```powershell
docker-compose -f docker-compose.database.yaml up
```

Once the MS SQL server is up and running run the docker file to create a Liquibase container and run an update command;

*note* at time of writing I've not been able to get dockers internal DNS to resolve the "ms-sql-net" network, the upshot of this is you have to follow these steps before you run the liquibase docker compose command;

1. Open a prompt to your WSL-2 Linux instance

2. Run this command;

   ```bash
   docker inspect ms-sql-db | grep IPAddress
   ```

3. This will give you an IP address, copy this into clipboard

4. Open liquibase.properties found in \config

5. Modify the following line;

   ```
   url: jdbc:sqlserver://ms-sql-net:1433;database=DataAccessExampleDatabase;trustServerCertificate=true;
   ```

6. Replace "ms-sql-net" with the IP address from step 3

7. Save the file

In the same folder open another command prompt and run;

```
docker-compose -f docker-compose.liquibase-update.yaml up
```

## Files

In the root of the MS-SQL folder we have the two docker compose yaml files we need to create the Postgres database server and Liquibase service containers.

### config

This folder contains the files required by Liquibase to run change scripts on the "DataAccessExampleDatabase" database

The files are as follows;

- changelog.xml - bootstrapping for Liquibase
- liquibase.properties - Liquibase configuration file (passwords etc)

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