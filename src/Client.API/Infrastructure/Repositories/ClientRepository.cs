using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.API.Infrastructure.Repositories
{
    public class ClientRepository
        : IClientRepository
    {
        private readonly ClientContext _clientContext;

        public ClientRepository(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }

        public async Task Insert(Models.Client client)
        {
            try
            {
                await _clientContext.AddAsync(client);
                await _clientContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Models.Client> GetByCpf(long cpf)
        {
            var client = await _clientContext.Clients.FirstOrDefaultAsync(c => c.Cpf == cpf);
            return client;
        }

        public async Task<IList<Models.Client>> GetAll()
        {
            IQueryable<Models.Client> queryable = _clientContext.Clients.AsNoTracking();
            var clients = await queryable.ToListAsync();
            return clients;
        }
    }
}
