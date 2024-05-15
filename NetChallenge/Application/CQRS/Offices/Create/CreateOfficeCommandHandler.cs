using MediatR;
using NetChallenge.Domain;
using System.Threading;
using System.Threading.Tasks;
using NetChallenge.Domain.Primitives;
using System;

namespace NetChallenge.Application.CQRS.Offices.Create
{
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOfficeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Unit> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = new Office
            {
                Name = request.Name,
                // Otras propiedades del objeto Office según sea necesario
            };

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}