using EmprestimosJogos.Domain.Tests.Fixture;
using Xunit;

namespace EmprestimosJogos.Domain.Tests.UnitTests
{
    [Collection(nameof(AmigoCollection))]
    public class AmigoTests
    {
        private readonly AmigoTestsFixture _amigoTestsFixture;

        public AmigoTests(AmigoTestsFixture amigoTestsFixture)
        {
            _amigoTestsFixture = amigoTestsFixture;
        }
    }
}
