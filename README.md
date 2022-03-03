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


## Configuration
In the ```appsettings.json``` file, you will need to at the bare-minimum supply a connection string under ````ConnectionStrings:Database````. You will also need to populate the ````Blob```` section with the bucket & folder.

## Migrations
This project uses entity framework to manage the data layer.

### Adding Migrations
Run the following command ```dotnet ef migrations add <migration_name>``` to add new migrations to the project
