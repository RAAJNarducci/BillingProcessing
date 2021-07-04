using Client.API.Utils.Validators;

namespace Client.API.Models
{
    public class Client : Entity
    {
        public string Nome { get; private set; }
        public string Estado { get; private set; }
        public long Cpf { get; private set; }

        protected Client() { }

        public Client(string nome, string estado, long cpf)
        {
            Nome = nome;
            Estado = estado;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new ClientValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
