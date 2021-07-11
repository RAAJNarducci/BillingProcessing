using AutoMapper;
using Client.API.Infrastructure;
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
        private readonly ClientContext _clientContext;
        private readonly IValidator<Models.Client> _clientValidator;

        public ClientService(IMapper mapper, ClientContext clientContext, IValidator<Models.Client> clientValidator)
        {
            _clientContext = clientContext;
            _mapper = mapper;
            _clientValidator = clientValidator;
        }

        public async Task<ClientResponse> Insert(ClientViewModel clientViewModel)
        {
            try
            {
                var client = _mapper.Map<Models.Client>(clientViewModel);
                ValidationResult validationResult = await _clientValidator.ValidateAsync(client);

                if (!validationResult.IsValid)
                    return new ClientResponse(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

                if (await IsClientDuplicated(client.Cpf))
                    return new ClientResponse(MSG_CLIENTE_JA_CADASTRADO);

                await _clientContext.AddAsync(client);
                await _clientContext.SaveChangesAsync();
                return new ClientResponse(clientViewModel);
            }
            catch (Exception ex)
            {
                return new ClientResponse(ex.Message);
            }
        }

        public async Task<ClientResponse> GetByCpf(long cpf)
        {
            var client = await _clientContext.Clients.FirstOrDefaultAsync(c => c.Cpf == cpf);
            return new ClientResponse(_mapper.Map<ClientViewModel>(client));
        }

        public async Task<ClientResponse> GetAll()
        {
            IQueryable<Models.Client> queryable = _clientContext.Clients.AsNoTracking();
            await queryable.ToListAsync();
            return new ClientResponse(_mapper.Map<IEnumerable<ClientViewModel>>(queryable));
        }

        private async Task<bool> IsClientDuplicated(long cpf)
        {
            var response = await GetByCpf(cpf);
            return response.Result is not null;
        }
    }
}
