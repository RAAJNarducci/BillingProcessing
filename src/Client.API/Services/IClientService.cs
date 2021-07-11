using Client.API.Models;
using System.Threading.Tasks;

namespace Client.API.Services
{
    public interface IClientService
    {
        Task<ClientResponse> Insert(ClientViewModel clientViewModel);
        Task<ClientResponse> GetByCpf(long cpf);
        Task<ClientResponse> GetAll();
    }
}
