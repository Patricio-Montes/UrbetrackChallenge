using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using System;

namespace NetChallenge.Test
{
    public class OfficeRentalServiceTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        protected OfficeRentalService Service;
        protected ILocationRepository LocationRepository;
        protected IOfficeRepository OfficeRepository;
        protected IBookingRepository BookingRepository;
        protected IMediator Mediator;

        public OfficeRentalServiceTest(TestFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

            var locationRepository = _fixture.ServiceProvider.GetRequiredService<ILocationRepository>();
            var officeRepository = _fixture.ServiceProvider.GetRequiredService<IOfficeRepository>();
            var bookingRepository = _fixture.ServiceProvider.GetRequiredService<IBookingRepository>();
            var mediator = _fixture.ServiceProvider.GetRequiredService<IMediator>();

            LocationRepository = locationRepository;
            OfficeRepository = officeRepository;
            BookingRepository = bookingRepository;

            Service = new OfficeRentalService(mediator);
        }
    }
}