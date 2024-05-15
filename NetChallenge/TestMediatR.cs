using MediatR;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Dto.Input;
using System;
using System.Threading.Tasks;

namespace NetChallenge
{
    public class TestMediatR
    {
        private readonly IMediator _mediator;
        public TestMediatR(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<string> TestMethod(AddLocationRequest request) 
        {
            try
            {
                var command = MapToAddLocationCommand(request);
                await _mediator.Send(command);
                return "Task.FromResult()";
            }
            catch (Exception ex)
            {
                await _mediator.Send(ex);
                return "ex";
            }
        }

        private CreateLocationCommand MapToAddLocationCommand(AddLocationRequest request)
        {
            return new CreateLocationCommand(request.Name, request.Neighborhood);
        }
    }
}
