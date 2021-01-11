using Bogus;
using EmprestimosJogos.Domain.Entities;
using System;
using Xunit;

namespace EmprestimosJogos.Domain.Tests.Fixture
{
    [CollectionDefinition(nameof(JogoCollection))]
    public class JogoCollection : ICollectionFixture<JogoTestsFixture>
    {
    }

    public class JogoTestsFixture : IDisposable
    {
        public void Dispose()
        {
        }

        internal Jogo GerarJogoValido()
        {
            Faker<Jogo> _jogo = new Faker<Jogo>()
                .CustomInstantiator(f => new Jogo
                {
                    Id = Guid.NewGuid(),
                    Nome = f.Name.Random.Words(f.Random.Int(1, 4)),
                    CreatorId = Guid.NewGuid()
                });

            return _jogo;
        }

        internal Jogo GerarJogoInvalido()
        {
            Faker<Jogo> _jogo = new Faker<Jogo>()
                .CustomInstantiator(f => new Jogo
                {
                    Id = Guid.NewGuid(),
                    CreatorId = Guid.NewGuid()
                });

            return _jogo;
        }
    }
}