using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panther.Core.Player;
using Panther.Core.Player.Settings;

namespace Panther.Core.DependencyInjection.Extensions
{
    public static class PlayerExtensions
    {
        public static IServiceCollection AddPlayer(IServiceCollection services)
        {
            return services
                .AddSingleton<AudioFileReaderAccessor>()
                .AddSingleton<IPlayerService, PlayerService>();
        }

        public static IServiceCollection AddPlayerQueue(IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<QueueSettings>(configuration.GetSection(nameof(QueueSettings)))
                .AddSingleton<IQueueService, QueueService>();
        }
    }
}
