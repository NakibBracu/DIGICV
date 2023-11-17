using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using DigiCV.EmailWorker;
using DigiCV.Infrastructure;
using DigiCV.Persistence;
using Serilog;
using Serilog.Events;
using DigiCV.Domain.Utilities;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables().Build();
var connectionString = configuration.GetConnectionString("DefaultConnection");
var migrationsAssembly = typeof(Worker).Assembly.FullName;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Connection String: {ConnectionString}", connectionString);
    Log.Information("Application Starting...");

    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .UseSerilog()
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterInstance<IConfiguration>(configuration);
            builder.Register(ctx =>
            {
                var config = ctx.Resolve<IConfiguration>();
                return Options.Create(config.GetSection("Smtp").Get<Smtp>());
            }).As<IOptions<Smtp>>();
            builder.RegisterModule(new WorkerModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new PersistenceModule(connectionString, migrationsAssembly));
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .Build();

    await host.RunAsync();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

