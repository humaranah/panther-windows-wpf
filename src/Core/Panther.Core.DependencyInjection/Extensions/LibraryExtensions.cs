using Microsoft.Extensions.DependencyInjection;
using Panther.Core.Data;
using System;

namespace Panther.Core.DependencyInjection.Extensions
{
    public static class LibraryExtensions
    {
        public static IServiceCollection AddLibraryServices(this IServiceCollection services)
        {
            return services;
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
