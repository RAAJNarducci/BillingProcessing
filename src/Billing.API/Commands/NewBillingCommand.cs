using BillingAPI.Models.Responses;
using BillingAPI.Utils.Validations;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace BillingAPI.Commands
{
    public class NewBillingCommand : Command, IRequest<BillingResponse>
    {
        public DateTime DataVencimento { get; set; }

        public double ValorCobranca { get; set; }

        public string Cpf { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new BillingValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
