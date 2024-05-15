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
            // Configuraci�n del mock de IMediator
            var mediatorMock = new Mock<IMediator>();

            // Configurar el mock para ejecutar una acci�n cuando se llame a Send
            mediatorMock
                .Setup(x => x.Send(It.IsAny<IRequest<Unit>>(), It.IsAny<CancellationToken>()))
                .Callback<IRequest<Unit>, CancellationToken>((request, cancellationToken) =>
                {
                    if (request is CreateLocationCommand createLocationCommand)
                    {
                        // Punto de interrupci�n para depurar
                        // Aqu� puedes establecer un punto de interrupci�n para depurar el manejador
                        // Esto te permitir� inspeccionar lo que sucede dentro del manejador cuando se env�a el comando
                    }
                });

            // Creaci�n de la instancia de TestMediatR con el mock de IMediator configurado
            var test = new TestMediatR(mediatorMock.Object);

            // Creaci�n de la solicitud
            var request = new AddLocationRequest { Name = "UnitTest", Neighborhood = "Default" };

            // Ejecuci�n del m�todo bajo prueba
            var result = await test.TestMethod(request);

            // No hay nada que verificar en este caso
            // Simplemente est�s depurando para verificar lo que sucede dentro del manejador
            // Por lo tanto, no es necesario hacer ninguna afirmaci�n aqu�
        }
    }
}