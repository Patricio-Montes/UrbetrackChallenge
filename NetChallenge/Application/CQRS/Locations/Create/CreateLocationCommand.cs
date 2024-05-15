using MediatR;

namespace NetChallenge.Application.CQRS.Locations.Create
{
    public record CreateLocationCommand(
        string Name,
        string Neighborhood) : IRequest<Unit>;
}
