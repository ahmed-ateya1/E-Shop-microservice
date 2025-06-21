using Basket.API.Data;
using Basket.API.Models;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Basket.API
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
                    .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "ahmed")
                    .Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "BasketDB");

                opts.Connection(connection);
                opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
            }).UseLightweightSessions();

            builder.Services.AddControllers();
            builder.Services.AddCarter();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<Program>();
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
            });

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            var rawConnStr = builder.Configuration.GetConnectionString("MartenConnection")!;
            var resolvedConnStr = rawConnStr
                .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost")
                .Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres")
                .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "ahmed")
                .Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "BasketDB");

            builder.Services.AddHealthChecks()
                .AddNpgSql(resolvedConnStr)
                .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}
