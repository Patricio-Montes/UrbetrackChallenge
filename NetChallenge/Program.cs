using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using NetChallenge.Application.Services;
using NetChallenge.Infrastructure;

namespace NetChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Configuración de servicios
            #region Repositories
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            #endregion

            #region Services
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IOfficeRentalService, OfficeRentalService>();
            #endregion

            return services.BuildServiceProvider();
        }
    }
}