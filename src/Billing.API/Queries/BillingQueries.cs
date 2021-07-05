using BillingAPI.Infrastructure.Repositories;
using BillingAPI.Models.Responses;
using BillingAPI.ViewModels;
using System.Threading.Tasks;

namespace BillingAPI.Queries
{
    public class BillingQueries
        : IBillingQueries
    {
        private readonly IBillingRepository _billingRepository;

        public BillingQueries(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public async Task<BillingResponse> Get(BillingViewModel billingViewModel)
        {
            var teste = await _billingRepository.Get(billingViewModel.Cpf, billingViewModel.DataVencimento);
            return new BillingResponse("Teste");
        }
    }
}
