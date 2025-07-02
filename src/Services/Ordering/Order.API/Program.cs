using Ordering.Infrastructure;
using Ordering.Application;
namespace Ordering.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices();

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            await app.UseApiSerices();

            app.Run();
        }
    }
}
