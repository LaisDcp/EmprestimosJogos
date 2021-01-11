using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Tests.Fixture;
using System.Collections.Generic;
using Xunit;

namespace EmprestimosJogos.Domain.Tests.UnitTests
{
    [Collection(nameof(JogoCollection))]
    public class JogoTests
    {
        private readonly JogoTestsFixture _jogoTestsFixture;

        public JogoTests(JogoTestsFixture jogoTestsFixture)
        {
            _jogoTestsFixture = jogoTestsFixture;
        }

        [Fact(DisplayName = "Novo Jogo válido")]
        [Trait("Categoria", "Jogo Testes")]
        public void Jogo_NovoJogo_DeveEstarValido()
        {
            // Arrange
            Jogo _jogo = _jogoTestsFixture.GerarJogoValido();

            // Act
            bool _result = _jogo.IsValid();

            // Assert 
            Assert.True(_result);
            Assert.Equal(0, _jogo.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Jogo inválido")]
        [Trait("Categoria", "Jogo Testes")]
        public void Jogo_NovoJogo_DeveEstarInvalido()
        {
            // Arrange
            Jogo _jogo = _jogoTestsFixture.GerarJogoInvalido();

            // Act
            bool _result = _jogo.IsValid();

            // Assert 
            Assert.False(_result);
            Assert.NotEqual(0, _jogo.ValidationResult.Errors.Count);
        }
    }
}
