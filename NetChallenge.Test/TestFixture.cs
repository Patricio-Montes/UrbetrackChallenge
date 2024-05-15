using System;
using MediatR;
using Moq;
using NetChallenge.Abstractions;
using NetChallenge.Domain.Primitives;

namespace NetChallenge.Test
{
    public class TestFixture : IDisposable
    {
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
        }

        public void Dispose()
        {
            // No es necesario limpiar recursos en este caso
        }
    }
}
