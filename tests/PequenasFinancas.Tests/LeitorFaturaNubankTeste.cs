using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Importacao;

namespace PequenasFinancas.Tests;

public sealed class LeitorFaturaNubankTeste
{
    private const string Cabecalho = "date,title,amount";

    [Fact]
    public void ArquivoSemOCabecalhoDoNubankNaoEhAceito()
        => Assert.Throws<FormatException>(() => LeitorFaturaNubank.Ler("data;descricao;valor\n01/07/2026;Padaria;10"));

    [Fact]
    public void ArquivoVazioNaoEhAceito()
        => Assert.Throws<FormatException>(() => LeitorFaturaNubank.Ler(string.Empty));

    [Fact]
    public void FaturaSemCompraNaoTrazNenhumaLinha()
        => Assert.Empty(LeitorFaturaNubank.Ler(MontarFatura("2026-07-30,Pagamento recebido,\"- 76,90\"")));

    [Fact]
    public void PagamentoEDescontoFicamDeFora()
    {
        IReadOnlyList<CompraDaFatura> compras = LeitorFaturaNubank.Ler(MontarFatura(
            "2026-07-30,Pagamento recebido,\"- 76,90\"",
            "2026-07-30,Desconto,\"- 5,28\"",
            "2026-07-30,Padaria do Bairro,\"19,90\""));

        CompraDaFatura compra = Assert.Single(compras);

        Assert.Equal("Padaria do Bairro", compra.Descricao);
    }

    [Fact]
    public void CompraAVistaVemComUmaParcela()
    {
        CompraDaFatura compra = LerCompraUnica("2026-07-15,Livraria Central,\"80,90\"");

        Assert.Equal(1, compra.QuantidadeParcelas);
        Assert.Equal(1, compra.NumeroDaParcelaNaFatura);
        Assert.Equal(80.90m, compra.ValorDaParcela);
        Assert.False(compra.EhParcelada);
    }

    [Fact]
    public void TituloComParcelaTrazADescricaoLimpaEAQuantidade()
    {
        CompraDaFatura compra = LerCompraUnica("2026-07-02,Loja do Bairro - Parcela 3/12,\"76,30\"");

        Assert.Equal("Loja do Bairro", compra.Descricao);
        Assert.Equal(3, compra.NumeroDaParcelaNaFatura);
        Assert.Equal(12, compra.QuantidadeParcelas);
        Assert.Equal(76.30m, compra.ValorDaParcela);
    }

    [Fact]
    public void ValorComMilharEhLidoCorretamente()
        => Assert.Equal(1858.60m, LerCompraUnica("2026-07-06,Movelaria Sul,\"1.858,60\"").ValorDaParcela);

    [Fact]
    public void ValorTotalMultiplicaAParcelaPelaQuantidade()
        => Assert.Equal(915.60m, LerCompraUnica("2026-07-02,Loja do Bairro - Parcela 3/12,\"76,30\"").ValorTotal);

    [Fact]
    public void ParcelasAdiantadasDaMesmaCompraViramUmaLinhaSo()
    {
        IReadOnlyList<CompraDaFatura> compras = LeitorFaturaNubank.Ler(MontarFatura(
            "2026-07-26,Relojoaria Norte - Parcela 5/10,\"49,80\"",
            "2026-07-26,Relojoaria Norte - Parcela 4/10,\"49,80\"",
            "2026-07-02,Relojoaria Norte - Parcela 3/10,\"49,80\""));

        CompraDaFatura compra = Assert.Single(compras);

        Assert.Equal(3, compra.NumeroDaParcelaNaFatura);
        Assert.Equal(10, compra.QuantidadeParcelas);
    }

    [Fact]
    public void ComprasAVistaNaMesmaLojaContinuamSeparadas()
    {
        IReadOnlyList<CompraDaFatura> compras = LeitorFaturaNubank.Ler(MontarFatura(
            "2026-07-25,Restaurante da Praca,\"50,00\"",
            "2026-07-18,Restaurante da Praca,\"50,00\""));

        Assert.Equal(2, compras.Count);
    }

    [Fact]
    public void PrimeiraParcelaVoltaOsMesesJaCobradosDaFatura()
    {
        CompraDaFatura compra = LerCompraUnica("2026-07-02,Loja do Bairro - Parcela 3/12,\"76,30\"");

        Assert.Equal(new Competencia(2026, 6), compra.CalcularCompetenciaDaPrimeiraParcela(new Competencia(2026, 8)));
    }

    [Fact]
    public void CompraAVistaCaiNoProprioMesDaFatura()
    {
        CompraDaFatura compra = LerCompraUnica("2026-07-15,Livraria Central,\"80,90\"");

        Assert.Equal(new Competencia(2026, 8), compra.CalcularCompetenciaDaPrimeiraParcela(new Competencia(2026, 8)));
    }

    [Fact]
    public void ComprasVemDaMaisNovaParaAMaisAntiga()
    {
        IReadOnlyList<CompraDaFatura> compras = LeitorFaturaNubank.Ler(MontarFatura(
            "2026-07-10,Mercado Bom Preco,\"142,27\"",
            "2026-07-28,Farmacia Central,\"44,00\""));

        Assert.Equal("Farmacia Central", compras[0].Descricao);
        Assert.Equal("Mercado Bom Preco", compras[1].Descricao);
    }

    [Fact]
    public void TodaCompraJaVemMarcadaParaImportar()
        => Assert.True(LerCompraUnica("2026-07-15,Livraria Central,\"80,90\"").Selecionada);

    [Fact]
    public void MesDaFaturaSaiDoNomeDoArquivo()
        => Assert.Equal(new Competencia(2026, 8), LeitorFaturaNubank.DeduzirMesDaFatura("Nubank_2026-08-09.csv"));

    [Fact]
    public void NomeDeArquivoSemDataNaoDeduzMesDaFatura()
        => Assert.Null(LeitorFaturaNubank.DeduzirMesDaFatura("fatura.csv"));

    private static CompraDaFatura LerCompraUnica(string linha)
        => Assert.Single(LeitorFaturaNubank.Ler(MontarFatura(linha)));

    private static string MontarFatura(params string[] linhas)
        => string.Join('\n', [Cabecalho, .. linhas]);
}
