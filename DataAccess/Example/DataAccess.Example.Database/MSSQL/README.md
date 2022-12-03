# Microsoft SQL Server

## Docker instructions

Start by running the docker file to create a MS-SQL server container;

```powershell
docker-compose -f docker-compose.ms-sql.yaml up
```

The yaml file defines the password for the SA account as "Passw0rd!?2223", if you want to change this you'll need to edit the "SA_PASSWORD" element.

Once MS-SQL server is up and running run the docker file to create a Liquibase container and run an update command;

```
docker-compose -f docker-compose.liquibase-update.yaml up
```

