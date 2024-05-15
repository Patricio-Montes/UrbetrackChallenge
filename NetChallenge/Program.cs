using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using NetChallenge.Application.Services;
using NetChallenge.Configurations;

namespace NetChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Configura los servicios
            var services = new ServiceCollection();
            DependencyConfiguration.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            // Ahora puedes resolver tus servicios
            var cacheService = serviceProvider.GetRequiredService<ICacheService>();
            var locationRepository = serviceProvider.GetRequiredService<ILocationRepository>();
            // Haz lo que necesites con tus servicios...
        }
    }
}
