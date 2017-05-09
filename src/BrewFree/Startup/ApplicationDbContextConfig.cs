using BrewFree.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewFree
{
    public static class ApplicationDbContextConfig
    {
        public static void UseApplicationDbContextAutoMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                SeedData.EnsureSeedData(context);
            }
        }
    }
}
