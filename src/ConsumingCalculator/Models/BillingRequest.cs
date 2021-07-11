using System;

namespace ConsumingCalculator.Models
{
    public class BillingRequest
    {
        public DateTime DataVencimento { get; set; }

        public double ValorCobranca { get; set; }

        public string Cpf { get; set; }
    }
}
