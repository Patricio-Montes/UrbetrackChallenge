using MediatR;
using Moq;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Application.Data;
using NetChallenge.Domain;
using NetChallenge.Infrastructure;

namespace NetChallenge.Test
{
    public class OfficeRentalServiceTest
    {
        protected OfficeRentalService Service;
        protected ILocationRepository LocationRepository;
        protected IOfficeRepository OfficeRepository;
        protected IBookingRepository BookingRepository;

        public OfficeRentalServiceTest()
        {
            var locationPersistenceMock = new Mock<IApplicationPersistence<Location>>();
            LocationRepository = new LocationRepository(locationPersistenceMock.Object);

            var officePersistenceMock = new Mock<IApplicationPersistence<Office>>();
            OfficeRepository = new OfficeRepository(officePersistenceMock.Object);
            BookingRepository = new BookingRepository();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateLocationCommand>(), default))
            .ReturnsAsync(Unit.Value);

            Service = new OfficeRentalService(mediatorMock.Object);
        }
    }
}