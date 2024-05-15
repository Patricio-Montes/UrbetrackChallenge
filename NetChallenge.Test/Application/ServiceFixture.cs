using MediatR;
using Moq;
using NetChallenge.Abstractions;
using NetChallenge.Domain.Primitives;
using System;

namespace NetChallenge.Test.Application
{
    public class ServiceFixture : IDisposable
    {
        public Mock<IBookingRepository> IBookingRepositoryMock { get; }
        public Mock<ILocationRepository> ILocationRepositoryMock { get; }
        public Mock<IOfficeRepository> IOfficeRepositoryMock { get; }
        public Mock<IUnitOfWork> IUnitOfWorkMock { get; }
        public Mock<IMediator> IMediatorMock { get; }


        public ServiceFixture()
        {
            IBookingRepositoryMock = new Mock<IBookingRepository>();
            ILocationRepositoryMock = new Mock<ILocationRepository>();
            IOfficeRepositoryMock = new Mock<IOfficeRepository>();
            IUnitOfWorkMock = new Mock<IUnitOfWork>();
            IMediatorMock = new Mock<IMediator>();

            // Configurar comportamientos simulados si es necesario
            // Por ejemplo:
            // IBookingRepositoryMock.Setup(repo => repo.Method()).Returns(someValue);
            // ILocationRepositoryMock.Setup(repo => repo.Method()).Returns(someOtherValue);
            // ...
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
