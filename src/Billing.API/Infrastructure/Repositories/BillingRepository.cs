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

        public async Task<IEnumerable<Billing>> Get(string cpf, string mesReferencia)
        {
            var filterBuilder = Builders<Billing>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrWhiteSpace(cpf))
            {
                var cpfFilter = filterBuilder.Eq(x => x.Cpf, long.Parse(cpf));
                filter &= cpfFilter;
            }

            if (!string.IsNullOrWhiteSpace(mesReferencia))
            {
                var splitMesReferencia = mesReferencia.Split('/');
                var start = new DateTime(int.Parse(splitMesReferencia[1]), int.Parse(splitMesReferencia[0]), 01);
                var end = new DateTime(int.Parse(splitMesReferencia[1]), int.Parse(splitMesReferencia[0]), DateTime.DaysInMonth(int.Parse(splitMesReferencia[1]), int.Parse(splitMesReferencia[0])));
                var dateFilter = filterBuilder.Gte(x => x.DataVencimento, new BsonDateTime(start)) &
                         filterBuilder.Lte(x => x.DataVencimento, new BsonDateTime(end));
                filter &= dateFilter;
            }

            return await _billingContext.Billings.Find(filter).ToListAsync();
        }
    }
}
