using BillingAPI.Utils.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace BillingAPI.ViewModels
{
    public class BillingViewModel
    {
        [DataType(DataType.Date, ErrorMessage = "Data inválida")]
        public DateTime DataVencimento { get; set; }

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
