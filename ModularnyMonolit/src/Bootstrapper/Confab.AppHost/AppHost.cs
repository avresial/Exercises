var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Confab_Bootstrapper>("confab-bootstrapper");

builder.Build().Run();
