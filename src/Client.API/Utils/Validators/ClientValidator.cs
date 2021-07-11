using FluentValidation;
using System.Text.RegularExpressions;

namespace Client.API.Utils.Validators
{
    public class ClientValidator : AbstractValidator<Models.Client>
    {
        public ClientValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("O campo Nome é obrigatório");
            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O campo Estado é obrigatório")
                .Length(2, 2).WithMessage("O Estado deve estar no padrão UF")
                .Must(IsEstadoFormatValid).WithMessage("UF inválido");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O campo Cpf é obrigatório")
                .Must(IsCpfValid).WithMessage("CPF inválido")
                .Must(IsCpfFormatValid).WithMessage("CPF no formato incorreto");
        }

        private static bool IsCpfValid(long cpf)
        {
            return CpfValidator.IsValid(cpf.ToString());
        }
        private static bool IsCpfFormatValid(long cpf)
        {
            return Regex.IsMatch(cpf.ToString(), RegexValidations.REGEX_CPF);
        }
        private static bool IsEstadoFormatValid(string estado)
        {
            return Regex.IsMatch(estado.ToString(), RegexValidations.REGEX_UF);
        }


    }
}
