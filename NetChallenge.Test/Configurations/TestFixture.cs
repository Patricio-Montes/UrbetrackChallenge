using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Application.Services;
using NetChallenge.Domain.Primitives;
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
        public Mock<IUnitOfWork> IUnitOfWorkMock { get; }
        public Mock<IMediator> IMediatorMock { get; }

        public TestFixture()
        {
            IBookingRepositoryMock = new Mock<IBookingRepository>();
            ILocationRepositoryMock = new Mock<ILocationRepository>();
            IOfficeRepositoryMock = new Mock<IOfficeRepository>();
            IUnitOfWorkMock = new Mock<IUnitOfWork>();
            IMediatorMock = new Mock<IMediator>();

            var services = new ServiceCollection();
            services.AddDistributedMemoryCache();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IApplicationPersistence, ApplicationPersistence>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            // No es necesario limpiar recursos en este caso
        }
    }
}
