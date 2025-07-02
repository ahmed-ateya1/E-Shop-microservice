﻿using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
                context.Database.MigrateAsync();
            }
            return app;
        }
    }
}
