var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var blobs = builder.AddAzureStorage("storage")
    .AddBlobs("blobs");

var apiService = builder.AddProject<Projects.DotnetAspireDemo_ApiService>("apiservice")
    .WithReference(blobs);

builder.AddProject<Projects.DotnetAspireDemo_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService);

builder.AddNpmApp("angular", "../DotnetAspireDemo.Angular")
    .WithReference(apiService)
    .WithServiceBinding(containerPort: 3000, scheme: "http", env: "PORT")
    .AsDockerfileInManifest();

builder.Build().Run();
