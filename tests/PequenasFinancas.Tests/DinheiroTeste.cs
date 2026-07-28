using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Tests;

public sealed class DinheiroTeste
{
    [Theory]
    [InlineData("699,6", 699.6)]
    [InlineData("699.6", 699.6)]
    [InlineData("1234,56", 1234.56)]
    [InlineData("1234.56", 1234.56)]
    [InlineData("1.234,56", 1234.56)]
    [InlineData("87", 87)]
    [InlineData(" 4 800,00 ", 4800.00)]
    public void AceitaPontoOuVirgulaComoSeparadorDecimal(string texto, double valorEsperado)
    {
        Assert.True(Dinheiro.TentarConverter(texto, out decimal valor));
        Assert.Equal((decimal)valorEsperado, valor);
    }

    [Theory]
    [InlineData("699,")]
    [InlineData("699.")]
    public void SeparadorNoFimEhTratadoComoValorInteiro(string texto)
    {
        Assert.True(Dinheiro.TentarConverter(texto, out decimal valor));
        Assert.Equal(699m, valor);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void TextoVazioViraZero(string? texto)
    {
        Assert.True(Dinheiro.TentarConverter(texto, out decimal valor));
        Assert.Equal(0m, valor);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("12abc")]
    [InlineData("1,2,3")]
    [InlineData("-50")]
    public void TextoInvalidoNaoEhConvertido(string texto)
    {
        Assert.False(Dinheiro.TentarConverter(texto, out _));
    }

    [Fact]
    public void FormatarParaEdicaoUsaVirgulaESempreDuasCasas()
    {
        Assert.Equal("699,60", Dinheiro.FormatarParaEdicao(699.6m));
        Assert.Equal("1234,00", Dinheiro.FormatarParaEdicao(1234m));
    }
}
