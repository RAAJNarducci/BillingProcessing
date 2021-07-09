using BillingAPI.Utils.Validations;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace BillingAPI.ViewModels
{
    public class BillingViewModel
    {
        [MonthValidation(ErrorMessage = "Formato inválido - Utilize MM/yyyy")]
        public string MesReferencia { get; set; }

        [RegularExpression(RegexValidations.REGEX_CPF, ErrorMessage = "CPF no formato incorreto")]
        [CpfValidation(ErrorMessage = "CPF inválido")]
        public string Cpf { get; set; }
    }

    public class CpfValidation : ValidationAttribute
    {
        public override bool IsValid(object cpf)
        {
            if (cpf is null)
                return true;
            return CpfValidator.IsValid(cpf.ToString());
        }
    }

    public class MonthValidation : ValidationAttribute
    {
        public override bool IsValid(object month)
        {
            if (month is null)
                return true;

            if (!month.ToString().Contains('/'))
                return false;

            var monthValue = month.ToString().Split('/');
            if (monthValue[0].Length != 2 && monthValue[1].Length != 4)
                return false;

            return true;
        }
    }

    public class BillingViewModelValidator : AbstractValidator<BillingViewModel>
    {
        public BillingViewModelValidator()
        {
            RuleFor(m => m.Cpf).NotEmpty().When(m => string.IsNullOrEmpty(m.MesReferencia));
            RuleFor(m => m.MesReferencia).NotEmpty().When(m => string.IsNullOrEmpty(m.Cpf));
        }
    }
}
