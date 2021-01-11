using Bogus;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using EmprestimosJogos.Services.Api;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmprestimosJogos.Services.ApiTests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>>
    {
    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public string UsuarioToken;
        public Guid UsuarioId;

        public readonly EmprestimosJogosFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            Factory = new EmprestimosJogosFactory<TStartup>();
            Client = Factory.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        internal NomeBaseViewModel GerarJogoValido()
        {
            Faker<NomeBaseViewModel> _jogo = new Faker<NomeBaseViewModel>()
                 .CustomInstantiator(f => new NomeBaseViewModel
                 (
                     nome: f.Name.Random.Words(f.Random.Int(1, 4))
                 ));

            return _jogo;
        }

        internal AmigoViewModel GerarAmigoValido()
        {
            Faker<AmigoViewModel> _amigo = new Faker<AmigoViewModel>()
                 .CustomInstantiator(f => new AmigoViewModel
                 (
                     nome: f.Person.FullName,
                     telefoneCelular: f.Phone.PhoneNumber("###########")
                 ));

            return _amigo;
        }

        internal CreateUsuarioViewModel GerarUsuarioValido()
        {
            string _senha = new Faker().Internet.Password(8);

            Faker<CreateUsuarioViewModel> _usuario = new Faker<CreateUsuarioViewModel>()
                 .CustomInstantiator(f => new CreateUsuarioViewModel
                 (
                     nome: f.Person.FullName,
                     email: f.Person.Email,
                     senha: _senha,
                     confirmacaoSenha: _senha
                 ));

            return _usuario;
        }

        public LoginViewModel GetLoginValido()
        {
            return new LoginViewModel
            (
                email: "adm@email.com.br",
                senha: "39ag86"
            );
        }

        public async Task RealizarLogin(LoginViewModel login)
        {
            Client = Factory.CreateClient();

            HttpResponseMessage _response = await Client.PostAsync("/api/v1/usuarios/autenticar", new StringContent(ExtensionMethodHelpers.ToJson(login),
                                                                                                                   Encoding.UTF8,
                                                                                                                   "application/json"));
            _response.EnsureSuccessStatusCode();
            var _result = new RetornoAutenticacaoViewModel();
            ExtensionMethodHelpers.TryDeserializeJson(await _response.Content.ReadAsStringAsync(), out _result);
            UsuarioToken = _result.Token;
            UsuarioId = _result.UsuarioId;
        }
    }
}
