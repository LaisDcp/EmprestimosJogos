using System;
using Xunit;

namespace EmprestimosJogos.Domain.Tests.Fixture
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection : ICollectionFixture<UsuarioTestsFixture>
    {
    }

    public class UsuarioTestsFixture : IDisposable
    {
        public void Dispose()
        {
        }
    }
}