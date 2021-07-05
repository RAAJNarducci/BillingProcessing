using BillingAPI.Models.Responses;
using BillingAPI.ViewModels;
using System.Threading.Tasks;

namespace BillingAPI.Queries
{
    public interface IBillingQueries
    {
        Task<BillingResponse> Get(BillingViewModel billingViewModel);
    }
}
