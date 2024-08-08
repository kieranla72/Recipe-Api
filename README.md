# sports-processing-api

## Entity framework core migrations
The database schema is managed code-first for this project, using EF core migrations. 
This means any time you update `DB/Migrations`, you will need to add a new migration.

You can do this by running `dotnet ef migrations add NewMigration --project ../DB`.

If you realise this migration was incorrect and you don't want to apply it, you can run
the command `dotnet ef migrations remove --project ../DB`.

Adding a migration does not update the database automatically, to do that you will need to
run the command `dotnet ef database update --project ../DB`

Connecting to the DB locally `mysql -h localhost -P 3306 --protocol=tcp -u root --password=example`