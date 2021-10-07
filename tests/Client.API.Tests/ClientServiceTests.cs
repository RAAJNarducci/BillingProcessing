using AutoMapper;
using Client.API.Controllers;
using Client.API.Infrastructure.Repositories;
using Client.API.Mappers;
using Client.API.Models;
using Client.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Client.API.Tests
{
    [Collection(nameof(ClientCollection))]
    public class ClientServiceTests
    {
        private static IMapper _mapper;
        readonly ClientTestFixtures _clientTestFixtures;

        public ClientServiceTests(ClientTestFixtures clientTestFixtures)
        {
            _clientTestFixtures = clientTestFixtures;
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        public async Task ClientService_Insert_ShouldIsSuccess()
        {
            var cliente = new Models.Client("Zé", "SP", 91869975057);
            var clienteViewModel = new ClientViewModel
            {
                Cpf = "91869975057",
                Estado = "SP",
                Nome = "Zé"
            };

            var clientRepository = new Mock<IClientRepository>();
            var validator = new Mock<IValidator<Models.Client>>();
            var clientServ = new ClientService(_mapper, clientRepository.Object);

            await clientServ.Insert(clienteViewModel);

            Assert.True(cliente.IsValid());
            clientRepository.Verify(r => r.GetByCpf(cliente.Cpf), Times.Once);
            clientRepository.Verify(r => r.Insert(cliente), Times.Once);
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

            ActionResult result = await clienteController.Post(clienteViewModel);

            clientService.Verify(r => r.Insert(clienteViewModel), Times.Once);
        }
    }
}
