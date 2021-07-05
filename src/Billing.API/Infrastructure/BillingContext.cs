using BillingAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BillingAPI.Infrastructure
{
    public class BillingContext
    {
        private readonly IMongoDatabase _database = null;

        public BillingContext(IOptions<BillingSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Billing> Billings
        {
            get
            {
                return _database.GetCollection<Billing>("billings");
            }
        }
    }
}
