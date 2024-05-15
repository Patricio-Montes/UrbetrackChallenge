using Castle.Core.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using NetChallenge.Infrastructure;

namespace NetChallenge.Test
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; }

        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuración del contenedor DI para tus pruebas
            // Por ejemplo, registrar tus servicios y dependencias necesarias aquí
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IOfficeRepository, OfficeRepository>();
            services.AddTransient<IMediator, Mediator>();
            // ...
        }
    }
}
