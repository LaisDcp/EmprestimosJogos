using Bogus;
using EmprestimosJogos.Domain.Entities;
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

        internal Usuario GerarUsuarioValido()
        {
            Faker<Usuario> _usuario = new Faker<Usuario>()
                 .CustomInstantiator(f => new Usuario
                 {
                     Id = Guid.NewGuid(),
                     Nome = f.Person.FullName,
                     Email = f.Person.Email,
                     PerfilId = Guid.NewGuid()
                 });

            return _usuario;
        }

        internal Usuario GerarUsuarioInvalido()
        {
            Faker<Usuario> _usuario = new Faker<Usuario>()
                .CustomInstantiator(f => new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = f.Person.FullName,
                    Email = f.Random.Word(),
                    PerfilId = Guid.NewGuid()
                });

            return _usuario;
        }
    }
}