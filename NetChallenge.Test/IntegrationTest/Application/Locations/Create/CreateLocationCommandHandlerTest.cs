using MediatR;
using NetChallenge.Application.CQRS.Locations.Create;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Abstractions;
using System.Linq;


namespace NetChallenge.Test.IntegrationTest.Application.Locations.Create
{
    public class CreateLocationCommandHandlerTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public CreateLocationCommandHandlerTest(TestFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public async Task Handle_Create_Should_VerifyItem_And_Return_Unit()
        {
            // Arrange
            var locationRepository = _fixture.ServiceProvider.GetRequiredService<ILocationRepository>();

            var handler = new CreateLocationCommandHandler(locationRepository);
            var request = new CreateLocationCommand("Location_Name", "Neighborhood_");

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var locationListResults = locationRepository.AsEnumerable();

            var locationResult = locationListResults.FirstOrDefault();

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.Single(locationListResults);

            Assert.NotNull(locationResult);

            Assert.Equal(request.Name, locationResult.Name);
            Assert.Equal(request.Neighborhood, locationResult.Neighborhood);
        }
    }
}