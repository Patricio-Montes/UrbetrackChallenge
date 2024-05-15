using MediatR;
using NetChallenge.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Offices.Create
{
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Unit>
    {

        public CreateOfficeCommandHandler()
        {
        }

        public async Task<Unit> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = new Office
            {
                Name = request.Name,
            };

            return Unit.Value;
        }
    }
}