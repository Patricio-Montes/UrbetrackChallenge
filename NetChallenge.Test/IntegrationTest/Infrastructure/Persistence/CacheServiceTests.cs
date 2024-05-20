using System;
using System.Threading.Tasks;
using NetChallenge.Application.Services;
using NetChallenge.Domain;
using NetChallenge.Infrastructure.Services;

namespace NetChallenge.Tests.Infrastructure.Services
{
    public class CacheServiceTests
    {
        private readonly ICacheService _cacheService;

        public CacheServiceTests()
        {
            _cacheService = new CacheService();
        }

        [Fact]
        public async Task SetAsync_ShouldStoreItemInCache()
        {
            // Arrange
            var booking = new Booking { Id = Guid.NewGuid(), DateTime = DateTime.Now, Duration = TimeSpan.FromHours(1) };

            // Act
            await _cacheService.SetAsync("booking1", booking);

            // Assert
            var cachedBooking = await _cacheService.GetAsync<Booking>("booking1");
            Assert.NotNull(cachedBooking);
            Assert.Equal(booking.Id, cachedBooking.Id);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNullForNonExistentKey()
        {
            // Act
            var result = await _cacheService.GetAsync<Booking>("nonexistent");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveItemFromCache()
        {
            // Arrange
            var booking = new Booking { Id = Guid.NewGuid(), DateTime = DateTime.Now, Duration = TimeSpan.FromHours(1) };
            await _cacheService.SetAsync("bookingToRemove", booking);

            // Act
            await _cacheService.RemoveAsync("bookingToRemove");
            var cachedBooking = await _cacheService.GetAsync<Booking>("bookingToRemove");

            // Assert
            Assert.Null(cachedBooking);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllItemsWithPrefix()
        {
            // Arrange
            var booking1 = new Booking { Id = Guid.NewGuid(), DateTime = DateTime.Now, Duration = TimeSpan.FromHours(1) };
            var booking2 = new Booking { Id = Guid.NewGuid(), DateTime = DateTime.Now, Duration = TimeSpan.FromHours(2) };

            await _cacheService.SetAsync("booking1", booking1);
            await _cacheService.SetAsync("booking2", booking2);

            // Act
            var allBookings = await _cacheService.GetAllAsync<Booking>("booking");

            // Assert
            Assert.NotNull(allBookings);
            Assert.Equal(2, allBookings.Count);
        }

        [Fact]
        public void Clear_ShouldRemoveAllItemsFromCache()
        {
            // Arrange
            var booking1 = new Booking { Id = Guid.NewGuid(), DateTime = DateTime.Now, Duration = TimeSpan.FromHours(1) };
            var booking2 = new Booking { Id = Guid.NewGuid(), DateTime = DateTime.Now, Duration = TimeSpan.FromHours(2) };

            _cacheService.SetAsync("booking1", booking1).Wait();
            _cacheService.SetAsync("booking2", booking2).Wait();

            // Act
            _cacheService.Clear();
            var allBookings = _cacheService.GetAllAsync<Booking>("booking").Result;

            // Assert
            Assert.Empty(allBookings);
        }
    }
}