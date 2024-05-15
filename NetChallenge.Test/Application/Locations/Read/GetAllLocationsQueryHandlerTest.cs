using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Application.CQRS.Locations.Read.GetAll;
using NetChallenge.Domain;
using Xunit;

namespace NetChallenge.Test.Application.Locations.Read.GetAll
{
    public class GetAllLocationsQueryHandlerTest : IClassFixture<ServiceFixture>
    {
        private readonly ServiceFixture _fixture;

        public GetAllLocationsQueryHandlerTest(ServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Handle_AddLocationThenGetAll_ReturnsAddedLocation()
        {
            // Arrange
            var createLocationHandler = new CreateLocationCommandHandler(_fixture.IUnitOfWorkMock.Object, _fixture.ILocationRepositoryMock.Object);

            var location = new Location
            {
                Id = Guid.NewGuid(),
                Name = "Test Location",
                Neighborhood = "Test Neighborhood"
            };

            await createLocationHandler.Handle(new CreateLocationCommand(location.Name, location.Neighborhood), CancellationToken.None);

            var getAllHandler = new GetAllLocationsQueryHandler(_fixture.ILocationRepositoryMock.Object);

            // Act
            var result = await getAllHandler.Handle(new GetAllLocationsQuery(), CancellationToken.None);

            // Assert
            Assert.Single(result); // Verifica que solo haya una ubicación en la lista
            Assert.Equal(location.Id, result.First().Id); // Verifica que la ubicación tenga el ID correcto
            Assert.Equal(location.Name, result.First().Name); // Verifica que la ubicación tenga el nombre correcto
            Assert.Equal(location.Neighborhood, result.First().Neighborhood); // Verifica que la ubicación tenga el barrio correcto
        }
    }
}