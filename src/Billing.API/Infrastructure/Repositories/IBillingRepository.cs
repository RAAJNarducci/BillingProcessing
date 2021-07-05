using BillingAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingAPI.Infrastructure.Repositories
{
    public interface IBillingRepository
    {
        Task Insert(Billing billing);
        Task<IEnumerable<Billing>> Get(string cpf, DateTime mesReferencia);
    }
}
