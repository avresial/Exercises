var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin();
var postgresdb = postgres.WithDataVolume().AddDatabase("ConfabDb");

builder.AddProject<Projects.Confab_Bootstrapper>("confab-bootstrapper")
    .WithReference(postgresdb);

builder.Build().Run();