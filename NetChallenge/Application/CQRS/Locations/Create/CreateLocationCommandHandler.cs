using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Locations.Create
{
    public sealed class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Unit>
    {
        private readonly ILocationRepository _locationRepository;

        public CreateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        public async Task<Unit> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new InvalidFieldException("Name");
            }

            if (string.IsNullOrEmpty(request.Neighborhood))
            {
                throw new InvalidFieldException("Neighborhood");
            }

            if (LocationExists(request.Name))
            {
                throw new ValidationException($"Location with name '{request.Name}' already exists.");
            }

            var location = new Location
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Neighborhood = request.Neighborhood
            };

            _locationRepository.Add(location);

            return Unit.Value;
        }

        private bool LocationExists(string name)
        {
            return _locationRepository.GetByName(name) is not null;
        }
    }
}
