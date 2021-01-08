using System;
using Xunit;

namespace EmprestimosJogos.Infra.CrossCutting.Helpers.Tests
{
    public class ExtensionMethodHelpersTests
    {
        [Fact(DisplayName = "Validar método de parcelamento")]
        [Trait("Categoria", "Validação GetParcelamento")]
        public void ExtensionMethodHelpers_ValidarMetodoGetParcelamento_DeveExecutarComSucesso()
        {
            // Arrange
            decimal _valorTotal = new Random().Next(50, 1000);
            (int quantidadeParcelas, decimal valorParcela) = ExtensionMethodHelpers.GetParcelamento(12, 10, _valorTotal);

            // Act
            bool _result = Math.Round(quantidadeParcelas * valorParcela, 0) == _valorTotal;

            // Assert 
            Assert.True(_result);
        }
    }
}
