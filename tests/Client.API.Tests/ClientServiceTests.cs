using Client.API.Controllers;
using Client.API.Models;
using Client.API.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Client.API.Tests
{
    public class ClientServiceTests
    {
        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        public async Task ClientService_Insert_ShouldIsSuccess()
        {
            var cliente = new Models.Client("Zé", "SP", 41281804835);
            var clienteViewModel = new ClientViewModel
            {
                Cpf = "41281804835",
                Estado = "SP",
                Nome = "Zé"
            };

            var clientService = new Mock<IClientService>();
            clientService.Setup(foo => foo.Insert(clienteViewModel)).ReturnsAsync(new ClientResponse(clienteViewModel));

            var clienteController = new ClientController(clientService.Object);

            var response = await clienteController.Post(clienteViewModel);

            clientService.Verify(r => r.Insert(clienteViewModel), Times.Once);
        }
    }
}
