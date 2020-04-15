using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Panther.Core.Data;
using System;
using System.Reflection;

namespace Panther.Core.DependencyInjection.Extensions
{
    public static class LibraryExtensions
    {
        public static IServiceCollection AddLibraryServices(this IServiceCollection services)
        {
            return services
                .AddDbContext<LibraryDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseSqlite("Data Source=library.db",
                        options =>
                        {
                            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        });
                });
        }

        public static void ConfigureLibrary(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>())
            {
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
