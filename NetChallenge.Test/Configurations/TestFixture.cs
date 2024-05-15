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
        public Mock<IBookingRepository> IBookingRepositoryMock { get; }
        public Mock<ILocationRepository> ILocationRepositoryMock { get; }
        public Mock<IOfficeRepository> IOfficeRepositoryMock { get; }
        public Mock<IMediator> IMediatorMock { get; }

        public TestFixture()
        {
            IBookingRepositoryMock = new Mock<IBookingRepository>();
            ILocationRepositoryMock = new Mock<ILocationRepository>();
            IOfficeRepositoryMock = new Mock<IOfficeRepository>();
            IMediatorMock = new Mock<IMediator>();

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
