using EmprestimosJogos.Services.Api;
using EmprestimosJogos.Services.ApiTests.Config;
using Xunit;

namespace EmprestimosJogos.Services.ApiTests.IntegrationTest
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        private string _baseEndpoint = "/api/v1/usuarios";

        public UsuarioTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

    }
}
