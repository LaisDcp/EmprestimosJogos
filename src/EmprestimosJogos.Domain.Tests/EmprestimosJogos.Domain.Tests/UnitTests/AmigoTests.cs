using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Tests.Fixture;
using Xunit;

namespace EmprestimosJogos.Domain.Tests.UnitTests
{
    [Collection(nameof(AmigoCollection))]
    public class AmigoTests
    {
        private readonly AmigoTestsFixture _amigoTestsFixture;

        public AmigoTests(AmigoTestsFixture amigoTestsFixture)
        {
            _amigoTestsFixture = amigoTestsFixture;
        }

        [Fact(DisplayName = "Novo Amigo válido")]
        [Trait("Categoria", "Amigo Testes")]
        public void Amigo_NovoAmigo_DeveEstarValido()
        {
            // Arrange
            Amigo _amigo = _amigoTestsFixture.GerarAmigoValido();

            // Act
            bool _result = _amigo.IsValid();

            // Assert 
            Assert.True(_result);
            Assert.Equal(0, _amigo.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Amigo inválido")]
        [Trait("Categoria", "Amigo Testes")]
        public void Jogo_NovoJogo_DeveEstarInvalido()
        {
            // Arrange
            Amigo _amigo = _amigoTestsFixture.GerarJogoInvalido();

            // Act
            bool _result = _amigo.IsValid();

            // Assert 
            Assert.False(_result);
            Assert.NotEqual(0, _amigo.ValidationResult.Errors.Count);
        }
    }
}
