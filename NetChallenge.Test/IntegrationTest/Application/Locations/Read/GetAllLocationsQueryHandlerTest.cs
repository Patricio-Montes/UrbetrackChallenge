using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Application.CQRS.Locations.Read.GetAll;
using NetChallenge.Domain;

namespace NetChallenge.Test.IntegrationTest.Application.Locations.Read
{
    public class GetAllLocationsQueryHandlerTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public GetAllLocationsQueryHandlerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Handle_AddLocationThenGetAll_ReturnsAddedLocation()
        {
            // Arrange
            var locationRepository = _fixture.ServiceProvider.GetRequiredService<ILocationRepository>();

            var location = new Location
            {
                Name = "Test Location",
                Neighborhood = "Test Neighborhood"
            };

            var createLocationHandler = new CreateLocationCommandHandler(locationRepository);

            await createLocationHandler.Handle(new CreateLocationCommand(location.Name, location.Neighborhood), CancellationToken.None);

            var getAllHandler = new GetAllLocationsQueryHandler(locationRepository);

            // Act
            var result = await getAllHandler.Handle(new GetAllLocationsQuery(), CancellationToken.None);

            // Assert
            Assert.Single(result); // Verifica que solo haya una ubicación en la lista
            Assert.Equal(location.Name, result.First().Name); // Verifica que la ubicación tenga el nombre correcto
            Assert.Equal(location.Neighborhood, result.First().Neighborhood); // Verifica que la ubicación tenga el barrio correcto
        }
    }
}