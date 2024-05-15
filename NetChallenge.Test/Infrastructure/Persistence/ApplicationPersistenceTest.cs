using NetChallenge.Infrastructure.Persistence;
using Moq;
using NetChallenge.Domain;
using System.Threading.Tasks;
using Xunit;
using NetChallenge.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Infrastructure.Services;
using NetChallenge.Application.Data;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace NetChallenge.Test.Infrastructure.Persistence
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
            // Configuración de servicios para el test de integración
            var services = new ServiceCollection();
            services.AddDistributedMemoryCache(); // Configura una caché en memoria para pruebas
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IApplicationPersistence, ApplicationPersistence>();
            var serviceProvider = services.BuildServiceProvider();

            // Arrange
            var location = new Location
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Test Name",
                Neighborhood = "Test Neighborhood"
            };

            // Obtiene una instancia de ApplicationPersistence desde el contenedor de servicios
            var persistence = serviceProvider.GetRequiredService<IApplicationPersistence>();

            // Act
            await persistence.AddAsync(location);
            var result = await persistence.GetAsync("Location");

            // Asserts
            Assert.NotNull(result);
            Assert.Single(result);

            // Verificar que el elemento en result sea de tipo Location
            var locationResults = result.Select(item =>
            {
                // Deserializa cada elemento a un objeto de tipo Location
                var locationJson = JsonConvert.SerializeObject(item);
                return JsonConvert.DeserializeObject<Location>(locationJson);
            }).ToList();

            var locationResult = locationResults.FirstOrDefault();

            Assert.NotNull(locationResult);

            // Verificar que el ID del elemento sea el esperado
            Assert.Equal(new Guid("00000000-0000-0000-0000-000000000001"), locationResult.Id);

            // Verificar que el elemento en result sea el mismo objeto que el objeto 'location' original
            Assert.Equal(location.Name, locationResult.Name);
            Assert.Equal(location.Neighborhood, locationResult.Neighborhood);
        }
    }
}
