using EmprestimosJogos.Services.Api;
using System;
using System.Net.Http;
using Xunit;

namespace EmprestimosJogos.Services.ApiTests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>>
    {
    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
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
    }
}
