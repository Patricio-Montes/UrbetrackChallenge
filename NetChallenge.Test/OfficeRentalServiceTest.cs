using MediatR;
using Moq;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
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
            var applicationPersistence = new Mock<IApplicationPersistence>();

            LocationRepository = new LocationRepository(applicationPersistence.Object);
            OfficeRepository = new OfficeRepository(applicationPersistence.Object);
            BookingRepository = new BookingRepository();

            var mediatorMock = new Mock<IMediator>();

            Service = new OfficeRentalService(mediatorMock.Object);
        }
    }
}