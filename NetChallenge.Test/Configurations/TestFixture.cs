using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using NetChallenge.Application.Configuration;
using NetChallenge.Application.Data;
using NetChallenge.Application.Services;
using NetChallenge.Infrastructure;
using NetChallenge.Infrastructure.Persistence;
using NetChallenge.Infrastructure.Services;

namespace NetChallenge.Test.Configurations
{
    public class TestFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; }

        public TestFixture()
        {
            var services = new ServiceCollection();
            services.AddDistributedMemoryCache();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IApplicationPersistence, ApplicationPersistence>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();

            ServiceProvider = services.BuildServiceProvider();

            ServiceProvider.GetRequiredService<IMediator>();
            ServiceProvider.GetRequiredService<ILocationRepository>();
        }

        public void Dispose()
        {
            if (ServiceProvider is not null && ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
