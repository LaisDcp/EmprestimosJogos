using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using EmprestimosJogos.Services.Api;
using EmprestimosJogos.Services.ApiTests.Config;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmprestimosJogos.Services.ApiTests.IntegrationTest
{
    [TestCaseOrderer("PriorityOrderer", "Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class JogoTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;

        private string _baseEndpoint = "/api/v1/jogos";

        public JogoTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Realizar cadastro de jogos com sucesso")]
        [Trait("Categoria", "Integração - Jogos")]
        public async Task Jogo_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            NomeBaseViewModel _jogo = _testsFixture.GerarJogoValido();
            await _testsFixture.RealizarLogin(_testsFixture.GetLoginValido());
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            _testsFixture.Client.AtribuirUsuarioId(_testsFixture.UsuarioId.ToString());

            // Act
            HttpResponseMessage _response = await _testsFixture.Client.PostAsync( _baseEndpoint, new StringContent(ExtensionMethodHelpers.ToJson(_jogo),
                                                                                                                    Encoding.UTF8,
                                                                                                                    "application/json"));

            // Assert
            _response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Realizar emprestimos de jogos com sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração - Jogos")]
        public async Task Jogo_RealizarEmprestimo_DeveExecutarComSucesso()
        {
            // Arrange
            Guid _id = StartupTestsExtension.GetJogo().Id;
            Guid _amigoId = StartupTestsExtension.GetAmigo().Id;
            string _endpoint = $"{_baseEndpoint}/{_id}/emprestar?amigoId={_amigoId}";

            await _testsFixture.RealizarLogin(_testsFixture.GetLoginValido());
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            HttpResponseMessage _response = await _testsFixture.Client.PutAsync(_endpoint, null);

            // Assert
            _response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Realizar devolução de jogos com sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integração - Jogos")]
        public async Task Jogo_RealizarDevolucao_DeveExecutarComSucesso()
        {
            // Arrange
            Guid _id = StartupTestsExtension.GetJogo().Id;
            string _endpoint = $"{_baseEndpoint}/{_id}/devolver";

            await _testsFixture.RealizarLogin(_testsFixture.GetLoginValido());
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            HttpResponseMessage _response = await _testsFixture.Client.PutAsync(_endpoint, null);

            // Assert
            _response.EnsureSuccessStatusCode();
        }
    }
}
