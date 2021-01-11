using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Tests.Fixture;
using Xunit;

namespace EmprestimosJogos.Domain.Tests.UnitTests
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioTests
    {
        private readonly UsuarioTestsFixture _usuarioTestsFixture;

        public UsuarioTests(UsuarioTestsFixture usuarioTestsFixture)
        {
            _usuarioTestsFixture = usuarioTestsFixture;
        }

        [Fact(DisplayName = "Novo Usuario válido")]
        [Trait("Categoria", "Usuario Testes")]
        public void Usuario_NovoUsuario_DeveEstarValido()
        {
            // Arrange
            Usuario _usuario = _usuarioTestsFixture.GerarUsuarioValido();

            // Act
            bool _result = _usuario.IsValid();

            // Assert 
            Assert.True(_result);
            Assert.Equal(0, _usuario.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Usuario inválido")]
        [Trait("Categoria", "Usuario Testes")]
        public void Usuario_NovoUsuario_DeveEstarInvalido()
        {
            // Arrange
            Usuario _usuario = _usuarioTestsFixture.GerarUsuarioInvalido();

            // Act
            bool _result = _usuario.IsValid();

            // Assert 
            Assert.False(_result);
            Assert.NotEqual(0, _usuario.ValidationResult.Errors.Count);
        }
    }
}
