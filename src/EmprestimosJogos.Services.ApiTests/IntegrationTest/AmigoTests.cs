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
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AmigoTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

        private string _baseEndpoint = "/api/v1/amigos";

        public AmigoTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Realizar cadastro de amigos com sucesso")]
        [Trait("Categoria", "Integração - Amigos")]
        public async Task Amigo_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            AmigoViewModel _amigo = _testsFixture.GerarAmigoValido();
            await _testsFixture.RealizarLogin(_testsFixture.GetLoginValido());
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            _testsFixture.Client.AtribuirUsuarioId(_testsFixture.UsuarioId.ToString());

            // Act
            HttpResponseMessage _response = await _testsFixture.Client.PostAsync( _baseEndpoint, new StringContent(ExtensionMethodHelpers.ToJson(_amigo),
                                                                                                                    Encoding.UTF8,
                                                                                                                    "application/json"));

            // Assert
            _response.EnsureSuccessStatusCode();
        }
    }
}
