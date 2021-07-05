using BillingAPI.Commands;
using BillingAPI.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BillingAPI.Utils.Validations
{
    public class BillingValidator : AbstractValidator<NewBillingCommand>
    {
        public BillingValidator()
        {
            RuleFor(c => c.DataVencimento).NotEmpty().WithMessage("O campo DataVencimento é obrigatório");
            RuleFor(c => c.ValorCobranca).NotEmpty().WithMessage("O campo ValorCobranca é obrigatório");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O campo Cpf é obrigatório")
                .Must(IsCpfValid).WithMessage("CPF inválido")
                .Must(IsCpfFormatValid).WithMessage("CPF no formato incorreto");
        }

        private static bool IsCpfValid(string cpf)
        {
            return CpfValidator.IsValid(cpf);
        }
        private static bool IsCpfFormatValid(string cpf)
        {
            return Regex.IsMatch(cpf.ToString(), RegexValidations.REGEX_CPF);
        }
    }
}
