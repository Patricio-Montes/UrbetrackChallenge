using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using NetChallenge.Infrastructure;

namespace NetChallenge.Test
{
    public class TestFixture : IDisposable
    {
        public ServiceProvider ServiceProvider { get; }

        public TestFixture()
        {
            var services = new ServiceCollection();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IOfficeRepository, OfficeRepository>();
            services.AddTransient<IMediator, Mediator>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            // No es necesario limpiar recursos en este caso
        }
    }
}
