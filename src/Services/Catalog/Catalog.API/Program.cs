
using Carter;
using Catalog.API.Models;
using Marten;
using Weasel.Core;

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

            builder.Services.AddCarter();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<Program>();
            });
            var app = builder.Build();
            app.MapCarter();
            app.Run();
        }
    }
}
