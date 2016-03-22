## Seeding Database
To seed users and their roles
> `update-database -ConfigurationTypeName OptionsWebsite.Migrations.IdentityMigrations.Configuration`

To seed diploma data
> `update-database -ConfigurationTypeName OptionsWebsite.Migrations.DiplomaMigrations.Configuration`


## Code 1st Migration Commands
> `enable-migrations -ContextProject DiplomaDataModel -ContextTypeName DiplomasContext -MigrationsDirectory Migrations\DiplomaMigrations`
> `add-migration -ConfigurationTypeName OptionsWebsite.Migrations.DiplomaMigrations.Configuration "InitialCreate"`
> `update-database -ConfigurationTypeName OptionsWebsite.Migrations.DiplomaMigrations.Configuration`