using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.API.Infrastructure.Repositories
{
    public interface IClientRepository
    {
        Task Insert(Models.Client client);
        Task<Models.Client> GetByCpf(long cpf);
        Task<IList<Models.Client>> GetAll();
    }
}
