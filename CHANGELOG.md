## v1.0.6 - 2019-11-23
### Refactored persistence layer registration

Instead of adding the IUnitOfWork through the service factory
```cs
services.AddTransient<IUnitOfWork>(_ => new UnitOfWork("connectionString"));
```

The ``IUnitOfWork`` contract is added to the service collection using the ``PersistenceExtension`` methods instead. This new feature allows us to switch between a Postgres Server or an Sql Server.

```cs
services.AddDatabase(builder => {
    builder.UseNpgsql("connectionString")
})
```

This initially features support for Postgres using ``Npgsql`` and SqlServer using ``System.Data.SqlClient``

This additional layer is done as in the future, the UnitOfWork registration will be added as a separate library and will be utilized in this template.

### Transfer service registration from Startup.cs to Installers

Moved all local service and worker registrations to the Installers folder for parity with [ApiBoilerPlate](https://github.com/proudmonkey/ApiBoilerPlate)