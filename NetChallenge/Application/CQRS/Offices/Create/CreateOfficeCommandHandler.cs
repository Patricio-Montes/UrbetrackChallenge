using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Offices.Create
{
    internal sealed class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Unit>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly ILocationRepository _locationRepository;

        public CreateOfficeCommandHandler(IOfficeRepository officeRepository, ILocationRepository locationRepository)
        {
            _officeRepository = officeRepository ?? throw new ArgumentNullException(nameof(officeRepository));
            _locationRepository = locationRepository ?? throw new ArgumentException(nameof(locationRepository));
        }

        public async Task<Unit> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            ValidateBusinessRules(request);

            var resources = request.AvailableResources
                .Select(item => new Resource
                {
                    Id = Guid.NewGuid(),
                    Description = item,
                    Available = true
                })
                .ToList();

            var office = new Office
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                MaxCapacity = request.MaxCapacity,
                Location = GetLocation(request.LocationName),
                Resources = resources
            };

            _officeRepository.Add(office);

            return Unit.Value;
        }

        private void ValidateBusinessRules(CreateOfficeCommand request)
        {
            if (string.IsNullOrEmpty(request.LocationName))
            {
                throw new InvalidFieldException("LocationName");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new InvalidFieldException("Name");
            }

            if (request.MaxCapacity <= 0)
            {
                throw new ValidationException("Max capacity must be greater than zero.");
            }

            var location = GetLocation(request.LocationName);
            if (location is null)
            {
                throw new ValidationException($"Location '{request.LocationName}' not found.");
            }

            if (OfficeExistsOnLocation(request.LocationName, request.Name))
            {
                throw new ValidationException($"Office with name '{request.Name}' already exists on location '{request.LocationName}'.");
            }
        }

        private Location GetLocation(string locationName)
        {
            return _locationRepository.GetByName(locationName);
        }

        private bool OfficeExistsOnLocation(string locationName, string officeName)
        {
            var office = _officeRepository
                .AsEnumerable()
                .FirstOrDefault(o => o.Location.Name == locationName && o.Name == officeName);

            return office != null;
        }
    }
}