var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var config = builder.Configuration;

var apiapp = builder.AddProject<Projects.backend>("apiapp")
                    .WithEnvironment("OpenAI__Endpoint", config["OpenAI:Endpoint"])
                    .WithEnvironment("OpenAI__ApiKey", config["OpenAI:ApiKey"])
                    .WithEnvironment("OpenAI__DeploymentName", config["OpenAI:DeploymentName"]);

builder.AddProject<Projects.frontend>("webapp")
       .WithExternalHttpEndpoints()
       .WithReference(cache)
       .WithReference(apiapp);

builder.Build().Run();
