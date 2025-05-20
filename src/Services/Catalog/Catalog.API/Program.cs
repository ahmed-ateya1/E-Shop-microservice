using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using Catalog.API.Products.CreateProduct;
using HealthChecks.UI.Client;
using Marten;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMarten(opts =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MartenConnection")!;

                var connection = connectionString
                    .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost")
                    .Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres")
                    .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password")
                    .Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "CatalogDB");

                opts.Connection(connection);
            }).UseLightweightSessions();

            if(builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<CatalogInitialData>();
            }

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
            builder.Services.AddCarter();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<Program>();
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("MartenConnection")!);


            var app = builder.Build();
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            app.UseHealthChecks("/health",new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
               ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}
