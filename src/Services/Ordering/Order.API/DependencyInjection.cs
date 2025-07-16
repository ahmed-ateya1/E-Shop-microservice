using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ordering.Infrastructure.Extensions;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks()
                .AddSqlServer(
                    connectionString: services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("OrderConnection")!,
                    name: "SQL Server",
                    failureStatus: HealthStatus.Unhealthy
                );
            return services;
        }
        public static async Task<WebApplication> UseApiSerices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                await app.DataBaseInitialzer();
                app.MapOpenApi();
            }
            app.MapCarter();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            
            app.UseExceptionHandler(options => { });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return app;
        }
    }
}
