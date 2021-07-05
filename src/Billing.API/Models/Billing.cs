using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BillingAPI.Models
{
    public class Billing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long Cpf { get; set; }
        public DateTime DataVencimento { get; set; }
        public double ValorCobranca { get; set; }
    }
}
