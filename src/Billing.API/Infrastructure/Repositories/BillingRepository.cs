using BillingAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingAPI.Infrastructure.Repositories
{
    public class BillingRepository
        : IBillingRepository
    {
        private readonly BillingContext _billingContext;

        public BillingRepository(IOptions<BillingSettings> settings)
        {
            _billingContext = new BillingContext(settings);
        }

        public async Task Insert(Billing billing)
        {
            await _billingContext.Billings.InsertOneAsync(billing);
        }

        public async Task<IEnumerable<Billing>> Get(string cpf, DateTime mesReferencia)
        {
            var filterCpf = Builders<Billing>.Filter.In("Cpf", cpf);
            var filterDate = Builders<Billing>.Filter.Gte(x => x.DataVencimento, new BsonDateTime(mesReferencia));
            var filter = Builders<Billing>.Filter.And(filterCpf, filterDate);
            return await _billingContext.Billings.Find(filter).ToListAsync();
        }
    }
}
