using System;
using Xunit;

namespace EmprestimosJogos.Domain.Tests.Fixture
{
    [CollectionDefinition(nameof(AmigoCollection))]
    public class AmigoCollection : ICollectionFixture<AmigoTestsFixture>
    {
    }

    public class AmigoTestsFixture : IDisposable
    {
        public void Dispose()
        {
        }
    }
}