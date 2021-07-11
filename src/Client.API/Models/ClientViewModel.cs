using Client.API.Utils.Validators;
using System.ComponentModel.DataAnnotations;

namespace Client.API.Models
{
    public class ClientViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [StringLength(2, ErrorMessage = "O Estado deve estar no padrão UF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Estado { get; set; }

        [RegularExpression(RegexValidations.REGEX_CPF, ErrorMessage = "CPF no formato incorreto")]
        [CpfValidation(ErrorMessage = "CPF inválido")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cpf { get; set; }
    }

    public class ClientCpfViewModel
    {
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
}
