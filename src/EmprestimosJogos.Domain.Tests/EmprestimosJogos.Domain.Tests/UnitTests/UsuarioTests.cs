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

    }
}
