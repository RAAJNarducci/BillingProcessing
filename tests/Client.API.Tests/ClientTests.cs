using Bogus;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Client.API.Tests
{
    public class ClientTests
    {
        [Fact(DisplayName = "Novo Cliente Válido")]
        public void Client_NewClient_ShouldIsValid()
        {
            var fakePerson = new Faker().Person;
            var client = new Models.Client(fakePerson.FirstName, "SP", 91570444064);

            var result = client.IsValid();

            Assert.True(result);
            Assert.Empty(client.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        public void Client_NewClient_ShouldIsInvalid()
        {
            var client = new Models.Client(null, "SP", 91570444064);

            var result = client.IsValid();

            Assert.False(result);
            Assert.NotEmpty(client.ValidationResult.Errors);
        }

        [Theory(DisplayName = "Novo Cliente - Nome Vazio ou Nulo")]
        [InlineData(null)]
        [InlineData("")]
        public void Client_NewClient_NomeNullOrEmpty(string nome)
        {
            var errorsMessage = new List<string>
            {
                 "O campo Nome é obrigatório"
            };

            var client = new Models.Client(nome, "SP", 91570444064);

            var result = client.IsValid();

            Assert.False(result);
            Assert.Equal(errorsMessage, client.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Theory(DisplayName = "Novo Cliente - Estado Vazio ou Nulo")]
        [InlineData(null)]
        [InlineData("")]
        public void Client_NewClient_EstadoNullOrEmpty(string estado)
        {
            var fakePerson = new Faker().Person;
            var errorsMessage = new List<string>
            {
                 "O campo Estado é obrigatório"
            };

            var client = new Models.Client(fakePerson.FirstName, estado, 91570444064);

            var result = client.IsValid();

            Assert.False(result);
            Assert.Equal(errorsMessage, client.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Theory(DisplayName = "Novo Cliente - Cpf Formato Incorreto")]
        [InlineData(412818048)]
        [InlineData(5241474)]
        public void Client_NewClient_CpfIncorrect(long cpf)
        {
            var fakePerson = new Faker().Person;
            var errorsMessage = new List<string>
            {
                 "CPF inválido",
                 "CPF no formato incorreto"
            };

            var client = new Models.Client(fakePerson.FirstName, "SP", cpf);

            var result = client.IsValid();

            Assert.False(result);
            Assert.Equal(errorsMessage, client.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Theory(DisplayName = "Novo Cliente - Cpf Inválido")]
        [InlineData(88877744411)]
        [InlineData(41281804800)]
        public void Client_NewClient_CpfInvalid(long cpf)
        {
            var fakePerson = new Faker().Person;
            var errorsMessage = new List<string>
            {
                 "CPF inválido"
            };

            var client = new Models.Client(fakePerson.FirstName, "SP", cpf);

            var result = client.IsValid();

            Assert.False(result);
            Assert.Equal(errorsMessage, client.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }
    }
}
