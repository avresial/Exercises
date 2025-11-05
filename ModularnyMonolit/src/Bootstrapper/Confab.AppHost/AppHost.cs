var builder = DistributedApplication.CreateBuilder(args);

//var username = builder.AddParameter("postgres", secret: true);
//var password = builder.AddParameter("secret", secret: true);

var postgres = builder.AddPostgres("pd").WithPgAdmin();
var postgresdb = postgres.AddDatabase("postgresdb");

builder.AddProject<Projects.Confab_Bootstrapper>("confab-bootstrapper")
    .WithReference(postgresdb);

builder.Build().Run();
