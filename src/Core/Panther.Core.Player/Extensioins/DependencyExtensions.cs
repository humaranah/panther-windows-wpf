using Microsoft.Extensions.DependencyInjection;

namespace Panther.Core.Player.Extensioins
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddPlayer(IServiceCollection services)
        {
            return services
                .AddSingleton<AudioFileReaderAccessor>()
                .AddSingleton<IPlayerService, PlayerService>();
        }
    }
}
