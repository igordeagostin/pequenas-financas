using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class ServicoParcelasTeste : IDisposable
{
    private static readonly Competencia PrimeiraParcela = new(2026, 7);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void CompraParceladaApareceEmTodosOsMesesDoParcelamento()
    {
        CadastrarCompraDeNotebookEm12Vezes();

        Assert.Equal(1, ObterNumeroDaParcelaEm(PrimeiraParcela));
        Assert.Equal(6, ObterNumeroDaParcelaEm(PrimeiraParcela.Adicionar(5)));
        Assert.Equal(12, ObterNumeroDaParcelaEm(PrimeiraParcela.Adicionar(11)));
    }

    [Fact]
    public void CompraNaoApareceAntesDaPrimeiraNemDepoisDaUltimaParcela()
    {
        CadastrarCompraDeNotebookEm12Vezes();

        Assert.Empty(_ambiente.Parcelas.ObterParcelasDeCartao(PrimeiraParcela.Anterior()));
        Assert.Empty(_ambiente.Parcelas.ObterParcelasDeCartao(PrimeiraParcela.Adicionar(12)));
    }

    [Fact]
    public void ParcelaTrazOCartaoEOProgressoParaExibicao()
    {
        CadastrarCompraDeNotebookEm12Vezes();

        ParcelaCalculada parcela = _ambiente.Parcelas
            .ObterParcelasDeCartao(PrimeiraParcela.Adicionar(2))
            .Single();

        Assert.Equal("Nubank", parcela.NomeCartao);
        Assert.Equal("3/12", parcela.Progresso);
        Assert.Equal(400.00m, parcela.Valor);
        Assert.Equal(new Competencia(2027, 6), parcela.UltimaCompetencia);
        Assert.False(parcela.EhUltimaParcela);
    }

    [Fact]
    public void CadaCartaoRecebeSomenteAsParcelasDasSuasCompras()
    {
        CompraCartao compraDoNubank = CadastrarCompraDeNotebookEm12Vezes();

        Cartao outroCartao = new() { Nome = "Inter", DiaVencimento = 10 };
        _ambiente.Cartoes.Salvar(outroCartao);

        _ambiente.ComprasCartao.Salvar(new CompraCartao
        {
            CartaoId = outroCartao.Id,
            Descricao = "Geladeira",
            ValorTotal = 2400.00m,
            QuantidadeParcelas = 6,
            CompetenciaPrimeiraParcela = PrimeiraParcela,
            Categoria = "Casa"
        });

        ParcelaCalculada parcelaDoNubank = _ambiente.Parcelas
            .ObterParcelasDoCartao(compraDoNubank.CartaoId, PrimeiraParcela)
            .Single();

        Assert.Equal("Notebook", parcelaDoNubank.Descricao);
        Assert.Equal(2, _ambiente.Parcelas.ObterParcelasDeCartao(PrimeiraParcela).Count);
    }

    [Fact]
    public void CalculaQuantoAindaFaltaPagarDeUmaCompra()
    {
        CompraCartao compra = CadastrarCompraDeNotebookEm12Vezes();

        Assert.Equal(4800.00m, ServicoParcelasEmAberto(compra, PrimeiraParcela));
        Assert.Equal(3600.00m, ServicoParcelasEmAberto(compra, PrimeiraParcela.Adicionar(3)));
        Assert.Equal(0m, ServicoParcelasEmAberto(compra, PrimeiraParcela.Adicionar(12)));
    }

    public void Dispose() => _ambiente.Dispose();

    private static decimal ServicoParcelasEmAberto(IParcelado parcelado, Competencia aPartirDe)
        => Core.Servicos.ServicoParcelas.CalcularValorEmAberto(parcelado, aPartirDe);

    private int ObterNumeroDaParcelaEm(Competencia competencia)
        => _ambiente.Parcelas.ObterParcelasDeCartao(competencia).Single().Numero;

    private CompraCartao CadastrarCompraDeNotebookEm12Vezes()
    {
        Cartao cartao = new() { Nome = "Nubank", DiaVencimento = 27 };
        _ambiente.Cartoes.Salvar(cartao);

        CompraCartao compra = new()
        {
            CartaoId = cartao.Id,
            Descricao = "Notebook",
            ValorTotal = 4800.00m,
            QuantidadeParcelas = 12,
            CompetenciaPrimeiraParcela = PrimeiraParcela,
            Categoria = "Eletrônicos"
        };

        _ambiente.ComprasCartao.Salvar(compra);

        return compra;
    }
}
