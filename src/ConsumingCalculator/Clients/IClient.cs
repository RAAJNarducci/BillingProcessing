using ConsumingCalculator.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumingCalculator.Clients
{
    public interface IClient
    {
        [Get("/api/cliente")]
        Task<IEnumerable<ClientResponse>> GetClients();
    }
}
