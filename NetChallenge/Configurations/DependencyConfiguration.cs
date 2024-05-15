using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using NetChallenge.Application.Services;
using NetChallenge.Infrastructure.Services;
using NetChallenge.Infrastructure;
using NetChallenge.Domain;

namespace NetChallenge.Configurations
{
    public static class DependencyConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<Booking>, BookingRepository>();
            services.AddScoped<IRepository<Location>, LocationRepository>();
            services.AddScoped<IRepository<Office>, OfficeRepository>();

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();

            services.AddScoped<ICacheService, CacheService>();
        }
    }
}
