using Autofac;
using Autofac.Extensions.DependencyInjection;
using DigiCV.API;
using DigiCV.Application;
using DigiCV.Infrastructure;
using DigiCV.Persistence;
using Serilog;
using Serilog.Events;
using System.Reflection;
using DigiCV.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DigiCV.Infrastructure.Securities.Permissions;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new PersistenceModule(connectionString,
            migrationAssembly));
        containerBuilder.RegisterModule(new ApiModule());
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddIdentity();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
            };
        });

    builder.Services.AddAuthorization(options =>
    {

        // Policy for only ADMIN or MANAGER 
        options.AddPolicy("AdminManager", policy =>
        {
            policy.AuthenticationSchemes.Clear();
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new AdminManagerRequirement());
        });

    });

    //builder.Services.AddCors(options =>
    //{
    //    options.AddPolicy("AllowWebApp",
    //        builder =>
    //        {
    //            builder.WithOrigins("https://localhost:7099", "https://localhost:7092", "http://localhost:8000", "http://localhost:9000", "http://localhost:80")
    //               .AllowAnyMethod()
    //               .AllowAnyHeader();
    //        });
    //});

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowWebApp",
            builder =>
            {
                builder
                    .AllowAnyOrigin() // Allow requests from any origin
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });

    // Activating dependency injection for claim based authorization
    builder.Services.AddSingleton<IAuthorizationHandler, AdminManagerRequirementHandler>();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //builder.WebHost.UseUrls("http://*:80"); //====   ====   ====   ====   ====   ====   ====   ====   ====    ====  ====
    var app = builder.Build();

    Log.Information("Connection String: {ConnectionString}", connectionString);
    Log.Information("API Application Starting...");

    //Configure the HTTP request pipeline.
    
    app.UseSwagger();
    app.UseSwaggerUI();
   
    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthorization(); 
    app.MapControllers(); 
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "API Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
