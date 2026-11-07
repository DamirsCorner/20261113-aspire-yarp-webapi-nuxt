using Aspire.Hosting.Yarp.Transforms;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Api>("api").WithExternalHttpEndpoints();

#pragma warning disable ASPIREJAVASCRIPT001
var nuxt = builder
    .AddViteApp("web", "../../nuxt")
    .WithPnpm()
    .PublishAsPackageScript()
    .WithExternalHttpEndpoints();
#pragma warning restore ASPIREJAVASCRIPT001

builder
    .AddYarp("gateway")
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute(nuxt);
        yarp.AddRoute("/api/{**catch-all}", api).WithTransformPathRemovePrefix("/api");
    });

builder.Build().Run();
