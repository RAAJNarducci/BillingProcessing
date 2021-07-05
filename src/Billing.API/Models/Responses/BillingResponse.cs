using BillingAPI.Utils;
using BillingAPI.ViewModels;

namespace BillingAPI.Models.Responses
{
    public class BillingResponse : BaseResponse<Billing>
    {
        public BillingResponse(Billing client) : base(client)
        {
        }

        public BillingResponse(string[] message) : base(message)
        {
        }

        public BillingResponse(string message) : base(new[] { message })
        {
        }
    }
}
