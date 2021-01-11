using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using EmprestimosJogos.Services.Api;
using EmprestimosJogos.Services.ApiTests.Config;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmprestimosJogos.Services.ApiTests.IntegrationTest
{
    [TestCaseOrderer("PriorityOrderer", "Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

        private string _baseEndpoint = "/api/v1/usuarios";

        public UsuarioTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Realizar cadastro de usuário com sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração - Usuário")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            CreateUsuarioViewModel _usuario = _testsFixture.GerarUsuarioValido();

            // Act
            HttpResponseMessage _response = await _testsFixture.Client.PostAsync( _baseEndpoint, new StringContent(ExtensionMethodHelpers.ToJson(_usuario),
                                                                                                                    Encoding.UTF8,
                                                                                                                    "application/json"));

            // Assert
            _response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Realizar login de usuário com sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração - Usuário")]
        public async Task Usuario_RealizarJogin_DeveExecutarComSucesso()
        {
            // Arrange
            LoginViewModel _login = _testsFixture.GetLoginValido();

            // Act
            HttpResponseMessage _response = await _testsFixture.Client.PostAsync($"{_baseEndpoint}/autenticar", new StringContent(ExtensionMethodHelpers.ToJson(_login),
                                                                                                                    Encoding.UTF8,
                                                                                                                    "application/json"));

            // Assert
            _response.EnsureSuccessStatusCode();
        }
    }
}
