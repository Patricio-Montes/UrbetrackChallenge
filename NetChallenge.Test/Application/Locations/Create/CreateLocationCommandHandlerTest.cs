using MediatR;
using Moq;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Domain.Primitives;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using NetChallenge.Domain;

namespace NetChallenge.Test.Application.Locations.Create
{
    public class CreateLocationCommandHandlerTest
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsUnit()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var locationRepositoryMock = new Mock<ILocationRepository>();

            locationRepositoryMock.Setup(repo => repo.Add(It.IsAny<Location>())).Returns(Task.FromResult(Unit.Value));

            var handler = new CreateLocationCommandHandler(unitOfWorkMock.Object, locationRepositoryMock.Object);
            var request = new CreateLocationCommand("Location Name", "Neighborhood");

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            locationRepositoryMock.Verify(repo => repo.Add(It.IsAny<Location>()), Times.Once);
            unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
