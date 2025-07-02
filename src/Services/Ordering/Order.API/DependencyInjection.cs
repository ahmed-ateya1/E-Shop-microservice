using Ordering.Infrastructure.Extensions;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services;
        }
        public static async Task<WebApplication> UseApiSerices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                await app.DataBaseInitialzer();
                app.MapOpenApi();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            return app;
        }
    }
}
