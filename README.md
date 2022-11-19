# Bmwadforth Backend

## Quick start

NOTE: All of these commands/steps should be run from the **project root**.

1. Create a ```.env``` with the following structure, replacing the username, password, etc.

```
POSTGRES_USER=[USERNAME]
POSTGRES_PASSWORD=[PASSWORD]
POSTGRES_DB=[DATABASE]
```
2. Set your ```GOOGLE_APPLICATION_CREDENTIALS``` environment variable so the application can talk to GCP blob storage.
3. Run the following command ```docker-compose up -d``` to start a local database server
4. Run the following command ```dotnet ef database update``` to ensure the migrations are applied to the database
5. Run the following command ```dotnet run``` to start the web server

### Frontend (Client App)
The frontend is sitting under ```ClientApp``` as a git submodule. When you clone this repository, make sure you clone recursively as to include the frontend repository. Simply starting the .NET app in an IDE (dotnet run) is sufficient enough to start the necessary scripts to launch the frontend and the backend together. 

## Configuration
In the ```appsettings.json``` file, you will need to at the bare-minimum supply a connection string under ````ConnectionStrings:Database````. You will also need to populate the ````Blob```` section with the bucket & folder.

## Migrations
This project uses entity framework to manage the data layer.

### Adding Migrations
Run the following command ```dotnet ef migrations add <migration_name>``` to add new migrations to the project

### Applying Migrations
Applying migrations in production can be found [here](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli).

## Deployment
When you merge into master, the github workflow located in the ```.github``` folder will run. It will build, publish and then deploy a docker image. The docker image simply builds the react app for production, then builds the .NET app, and puts their assets into a dockerfile. This dockerfile is then executed on google cloud platform cloud run.

The cloud run instance sits behind Google cloud services including a load balancer and API gateway. 