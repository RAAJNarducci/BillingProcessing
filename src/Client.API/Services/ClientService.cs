using AutoMapper;
using Client.API.Infrastructure;
using Client.API.Infrastructure.Repositories;
using Client.API.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.API.Services
{
    public class ClientService
        : IClientService
    {
        private const string MSG_CLIENTE_JA_CADASTRADO = "Cliente já cadastrado";

        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public ClientService(IMapper mapper, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientResponse> Insert(ClientViewModel clientViewModel)
        {
            try
            {
                var client = _mapper.Map<Models.Client>(clientViewModel);

                if (!client.IsValid())
                    return new ClientResponse(client.ValidationResult.Errors.Select(x => x.ErrorMessage).ToArray());

                if (await IsClientDuplicated(client.Cpf))
                    return new ClientResponse(MSG_CLIENTE_JA_CADASTRADO);

                await _clientRepository.Insert(client);

                return new ClientResponse(clientViewModel);
            }
            catch (Exception ex)
            {
                return new ClientResponse(ex.Message);
            }
        }

        public async Task<ClientResponse> GetByCpf(long cpf)
        {
            var client = await _clientRepository.GetByCpf(cpf);
            return new ClientResponse(_mapper.Map<ClientViewModel>(client));
        }

        public async Task<ClientResponse> GetAll()
        {
            var clients = await _clientRepository.GetAll();
            return new ClientResponse(_mapper.Map<IEnumerable<ClientViewModel>>(clients));
        }

        private async Task<bool> IsClientDuplicated(long cpf)
        {
            var response = await GetByCpf(cpf);
            return response.Result is not null;
        }
    }
}
