
using Carter;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
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
