using Client.API.Controllers;
using Client.API.Models;
using Client.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Client.API.Tests
{
    public class ClientServiceTests
    {
        public HttpClient Client;

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

        [Fact(DisplayName = "Adicionar Cliente com Inválido")]
        public async Task ClientService_Insert_ShouldIsError()
        {
            var cliente = new Models.Client("Zé", "SP", 41281804830);
            var clienteViewModel = new ClientViewModel
            {
                Cpf = "41281804830",
                Estado = "SP",
                Nome = "Zé"
            };

            var listErrors = new List<string>
            {
                "CPF inválido"
            };

            var clientService = new Mock<IClientService>();
            clientService.Setup(foo => foo.Insert(clienteViewModel)).ReturnsAsync(new ClientResponse(listErrors.ToArray()));

            var clienteController = new ClientController(clientService.Object);

            ActionResult result  = await clienteController.Post(clienteViewModel);

            clientService.Verify(r => r.Insert(clienteViewModel), Times.Once);
        }
    }
}
