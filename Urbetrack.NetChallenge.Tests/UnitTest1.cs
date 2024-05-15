using MediatR;
using Moq;
using NetChallenge;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Dto.Input;

namespace Urbetrack.NetChallenge.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddLocationTest()
        {
            // Configuración del mock de IMediator
            var mediatorMock = new Mock<IMediator>();

            // Configurar el mock para ejecutar una acción cuando se llame a Send
            mediatorMock
                .Setup(x => x.Send(It.IsAny<IRequest<Unit>>(), It.IsAny<CancellationToken>()))
                .Callback<IRequest<Unit>, CancellationToken>((request, cancellationToken) =>
                {
                    if (request is CreateLocationCommand createLocationCommand)
                    {
                        // Punto de interrupción para depurar
                        // Aquí puedes establecer un punto de interrupción para depurar el manejador
                        // Esto te permitirá inspeccionar lo que sucede dentro del manejador cuando se envía el comando
                    }
                });

            // Creación de la instancia de TestMediatR con el mock de IMediator configurado
            var test = new TestMediatR(mediatorMock.Object);

            // Creación de la solicitud
            var request = new AddLocationRequest { Name = "UnitTest", Neighborhood = "Default" };

            // Ejecución del método bajo prueba
            var result = await test.TestMethod(request);

            // No hay nada que verificar en este caso
            // Simplemente estás depurando para verificar lo que sucede dentro del manejador
            // Por lo tanto, no es necesario hacer ninguna afirmación aquí
        }
    }
}