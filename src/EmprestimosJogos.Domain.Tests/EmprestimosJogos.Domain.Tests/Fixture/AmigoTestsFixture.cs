using Bogus;
using EmprestimosJogos.Domain.Entities;
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

        internal Amigo GerarAmigoValido()
        {
            Faker<Amigo> _amigo = new Faker<Amigo>()
                .CustomInstantiator(f => new Amigo
                {
                    Id = Guid.NewGuid(),
                    Nome = f.Person.FullName,
                    CreatorId = Guid.NewGuid(),
                    TelefoneCelular = f.Phone.PhoneNumber("###########")
                });

            return _amigo;
        }

        internal Amigo GerarJogoInvalido()
        {
            Faker<Amigo> _amigo = new Faker<Amigo>()
                .CustomInstantiator(f => new Amigo
                {
                    Id = Guid.NewGuid(),
                    Nome = f.Person.FullName,
                    TelefoneCelular = f.Phone.PhoneNumber("###########")
                });

            return _amigo;
        }
    }
}