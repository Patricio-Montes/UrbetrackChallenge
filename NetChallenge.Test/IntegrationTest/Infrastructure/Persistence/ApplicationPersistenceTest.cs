using NetChallenge.Domain;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Newtonsoft.Json;
using NetChallenge.Application.Data;

namespace NetChallenge.Test.IntegrationTest.Infrastructure.Persistence
{
    public class ApplicationPersistenceTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public ApplicationPersistenceTest(TestFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public async Task IntegrationTest_AddItem_To_Cache_And_GetAll_Returns_Added_Location()
        {
            // Arrange
            var location = new Location
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Test Name",
                Neighborhood = "Test Neighborhood"
            };

            var persistence = _fixture.ServiceProvider.GetRequiredService<IApplicationPersistence>();

            // Act
            await persistence.AddAsync(location);
            var result = await persistence.GetAsync("Location");

            var locationListResults = result.Select(item =>
            {
                // Deserializa cada elemento a un objeto de tipo Location
                var locationJson = JsonConvert.SerializeObject(item);
                return JsonConvert.DeserializeObject<Location>(locationJson);
            }).ToList();

            var locationResult = locationListResults.FirstOrDefault();

            // Asserts
            Assert.NotNull(result);
            Assert.Single(result);

            Assert.NotNull(locationResult);

            Assert.Equal(new Guid("00000000-0000-0000-0000-000000000001"), locationResult.Id);

            Assert.Equal(location.Name, locationResult.Name);
            Assert.Equal(location.Neighborhood, locationResult.Neighborhood);
        }
    }
}
