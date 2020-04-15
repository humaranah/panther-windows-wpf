using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Panther.Core.Data;
using Panther.Core.Models;
using System.Reflection;

namespace Panther.Core.DependencyInjection.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            return services
                .AddDbContext<LibraryDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseSqlite("Data Source=library.db",
                        options =>
                        {
                            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        });
                })
                .AddScoped<IRepository<Artist>, LibraryRepository<Artist>>()
                .AddScoped<IRepository<Album>, LibraryRepository<Album>>()
                .AddScoped<IRepository<Song>, LibraryRepository<Song>>()
                .AddScoped<IRepository<Playlist>, LibraryRepository<Playlist>>();
        }
    }
}
