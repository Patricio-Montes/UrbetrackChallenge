using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Domain.Primitives;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Locations.Create
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocationRepository _locationRepository;

        public CreateLocationCommandHandler(IUnitOfWork unitOfWork, ILocationRepository locationRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        public async Task<Unit> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var location = new Location
                {
                    Name = request.Name,
                    Neighborhood = request.Neighborhood
                };

                await _locationRepository.Add(location);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
