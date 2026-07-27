using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Tests;

public sealed class RateioParcelasTeste
{
    [Fact]
    public void UltimaParcelaAbsorveOsCentavosQueSobram()
    {
        IReadOnlyList<decimal> parcelas = RateioParcelas.Calcular(100.00m, 3);

        Assert.Equal([33.33m, 33.33m, 33.34m], parcelas);
    }

    [Fact]
    public void SomaDasParcelasSempreFechaComOValorTotal()
    {
        IReadOnlyList<decimal> parcelas = RateioParcelas.Calcular(4800.00m, 12);

        Assert.Equal(4800.00m, parcelas.Sum());
        Assert.All(parcelas, parcela => Assert.Equal(400.00m, parcela));
    }

    [Fact]
    public void CompraSemParcelamentoGeraUmaParcelaComOValorInteiro()
    {
        IReadOnlyList<decimal> parcelas = RateioParcelas.Calcular(87.90m, 1);

        Assert.Equal([87.90m], parcelas);
    }

    [Theory]
    [InlineData(1, 33.33)]
    [InlineData(2, 33.33)]
    [InlineData(3, 33.34)]
    public void CalculaOValorDeUmaParcelaEspecifica(int numeroDaParcela, double valorEsperado)
    {
        decimal valor = RateioParcelas.CalcularParcela(100.00m, 3, numeroDaParcela);

        Assert.Equal((decimal)valorEsperado, valor);
    }

    [Fact]
    public void QuantidadeDeParcelasInvalidaEhRecusada()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RateioParcelas.Calcular(100m, 0));
    }
}
