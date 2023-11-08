﻿using AutoWrapper;
using DomainLayer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using Serilog;
using Serilog.Events;
using ServiceLayer.Business;
using WebApiLayer.Configurations.AppConfig;


namespace WebApiLayer.Configurations
{
    public static class AppConfigurationService
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IActivityServices, ActivityServices>();
            services.AddScoped<IActivityTypeServices, ActivityTypeServices>();
            services.AddScoped<IAssetAttributeServices, AssetAttributeServices>();
            services.AddScoped<IAssetServices, AssetServices>();
            services.AddScoped<IAssetTypeServices, AssetTypeServices>();
            services.AddScoped<IAttributeGroupServices, AttributeGroupServices>();
            services.AddScoped<ICharacterAssetServices, CharacterAssetServices>();
            services.AddScoped<ICharacterAttributeServices, CharacterAttributeServices>();
            services.AddScoped<ICharacterServices, CharacterServices>();
            services.AddScoped<ICharacterTypeServices, CharacterTypeServices>();
            services.AddScoped<IGameServerServices, GameServerServices>();
            services.AddScoped<IGameServices, GameServices>();
            services.AddScoped<ILevelProgressServices, LevelProgressServices>();
            services.AddScoped<ILevelServices, LevelServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWalletCategoryServices, WalletCategoryServices>();
            services.AddScoped<IWalletServices, WalletServices>();
        }
        public static void AddDbServices(this IServiceCollection services)
        {
            var settings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
            Console.WriteLine(settings);
            // use Console buildin logger to prevent EF log write to DB stream
            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseSqlServer(settings.Value.ConnectionStrings.DefaultConnection)
                    .LogTo(Console.WriteLine,
                        new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information,
                        DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.UtcTime)
            );
            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetService<ApplicationDbContext>()
            );
        }
        public static void AddCORS(this IServiceCollection services)
        {
            services.AddCors(p => p.AddPolicy("Cors", build =>
            {
                build.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
            }));
        }
        public static WebApplication UseAutoWrapper(this WebApplication app)
        {
            app.UseApiResponseAndExceptionWrapper(
                new AutoWrapperOptions
                {
                    IsApiOnly = false,
                    ShowIsErrorFlagForSuccessfulResponse = true,
                    WrapWhenApiPathStartsWith = $"/{Constants.HTTP.API_VERSION}",
                }
            );
            return app;
        }
        public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder, IConfiguration configuration) {
            builder.Host.UseSerilog((cntxt, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(configuration);
            });
            return builder;
        }
        public static WebApplication UseLoggingInterceptor(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });
            return app;
        }
        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        public static async Task DbInitializer(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await DatabaseInitializer.InitializeAsync(dbContext);
        }
    }
}
