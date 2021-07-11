using ConsumingCalculator.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumingCalculator.Clients
{
    public interface IBilling
    {
        [Post("/api/billing")]
        Task CreateBilling([Body] BillingRequest request);
    }
}
