using Autofac.Extensions.DependencyInjection;
using Autofac;
using DigiCV.Web;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;
using DigiCV.Persistence;
using DigiCV.Infrastructure;
using DigiCV.Application;
using DigiCV.Persistence.Extensions;
using DigiCV.Infrastructure.Securities.Permissions;
using Microsoft.AspNetCore.Authorization;
using DigiCV.Domain.Utilities;
using DinkToPdf.Contracts;
using DinkToPdf;
using DigiCV.Web.Models.PDF;
using DigiCV.Web.Models;

using Autofac.Core;

using DigiCV.Infrastructure.Utilities;
using Autofac.Core;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hbc, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration));

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
    {
        cb.RegisterModule(new ApplicationModule());
        cb.RegisterModule(new InfrastructureModule());
        cb.RegisterModule(new PersistenceModule(connectionString, migrationAssembly));
        cb.RegisterModule(new WebModule());

    });

    // Add services to the container. 
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddIdentity();
    builder.Services.AddControllersWithViews();
    builder.Services.Configure<Smtp>(builder.Configuration.GetSection("Smtp"));
    builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

    builder.Services.AddRouting(options =>
    {
        options.ConstraintMap["slugify"] = typeof(SlugifyRouteParameterTransformer);
    });
   
    // Authorization using user claim
    builder.Services.AddAuthorization(options =>
    {
        // Policy for ADMIN only
        options.AddPolicy("Administrator", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("Administrator", "Administrator");
        });

        // Policy for MANAGER only
        options.AddPolicy("Manager", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("Manager", "Manager");
        });

        // Policy for only ADMIN or MANAGER 
        options.AddPolicy("AdminManager", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new AdminManagerRequirement());
        });

        // Policy for USER only
        options.AddPolicy("User", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("User", "User");
        });

       
    });

    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    builder.Services.AddSingleton<IAuthorizationHandler, AdminManagerRequirementHandler>();
    //builder.WebHost.UseUrls("http://*:80");    //====   ====   ====   ====   ====   ====   ====   ====   ====    ====  ====

    var app = builder.Build();
    {

        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:slugify:exists}/{controller:slugify=Dashboard}/{action:slugify=Index}/{id?}");

        app.MapControllerRoute(
            name: "resume",
            pattern: "{controller:slugify=Resume}/{action:slugify=Share}/{username:slugify}/{resumeTitle:slugify}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");

        app.MapRazorPages();

        Log.Information("Connection String: {ConnectionString}", connectionString);
        Log.Information("Application is starting...");

        app.Run();
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application can not start.");
}
finally
{
    Log.CloseAndFlush();
}
