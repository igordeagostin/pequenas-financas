using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.Tests;

public sealed class ParcelaRemovidaTeste : IDisposable
{
    private static readonly Competencia PrimeiraParcela = new(2026, 7);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void ParcelaRemovidaNaoPesaNoMesDela()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela.Adicionar(2));

        Assert.Equal(0m, _ambiente.ComprasCartao.CalcularTotalDoMes(null, PrimeiraParcela.Adicionar(2)));
    }

    [Fact]
    public void OutrasParcelasContinuamNosMesesDelas()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela.Adicionar(2));

        Assert.Equal(400.00m, _ambiente.ComprasCartao.CalcularTotalDoMes(null, PrimeiraParcela.Adicionar(1)));
        Assert.Equal(400.00m, _ambiente.ComprasCartao.CalcularTotalDoMes(null, PrimeiraParcela.Adicionar(3)));
    }

    [Fact]
    public void FaltaPagarDesconsideraAParcelaRemovida()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela.Adicionar(2));

        Assert.Equal(4400.00m, _ambiente.ComprasCartao.CalcularSaldoDevedor(compra.CartaoId, PrimeiraParcela));
    }

    [Fact]
    public void ValorQueSobrouDesconsideraAsParcelasRemovidas()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela);
        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela.Adicionar(11));

        Assert.Equal(4000.00m, ServicoParcelas.CalcularValorQueSobrou(RecarregarCompra(compra)));
    }

    [Fact]
    public void CompraSaiDaListaDoMesDaParcelaRemovida()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela);

        Assert.Empty(_ambiente.ComprasCartao.ListarDoMes(PrimeiraParcela));
        Assert.Single(_ambiente.ComprasCartao.ListarDoMes(PrimeiraParcela.Proxima()));
    }

    [Fact]
    public void ParcelaRemovidaSaiDoResumoDoMes()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela);

        Assert.Equal(0m, _ambiente.Resumo.Calcular(PrimeiraParcela).TotalCartoes);
    }

    [Fact]
    public void RemoverAUltimaParcelaQueSobrouApagaACompra()
    {
        CompraCartao compra = CadastrarCompraDeUmaParcela();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela);

        Assert.Empty(_ambiente.ComprasCartao.Listar());
    }

    [Fact]
    public void RemoverParcelaDeMesSemParcelaNaoMudaNada()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela.Anterior());

        Assert.Empty(RecarregarCompra(compra).ParcelasRemovidas);
    }

    [Fact]
    public void RemoverAMesmaParcelaDuasVezesNaoRepeteORegistro()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela);
        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela);

        Assert.Single(RecarregarCompra(compra).ParcelasRemovidas);
    }

    [Fact]
    public void ContagemDeParcelasQueSobraramDesconsideraAsRemovidas()
    {
        CompraCartao compra = CadastrarNotebookDe4800Em12Vezes();

        _ambiente.ComprasCartao.RemoverParcela(compra.Id, PrimeiraParcela.Adicionar(4));

        Assert.Equal(11, ServicoParcelas.ContarParcelasQueSobraram(RecarregarCompra(compra)));
    }

    public void Dispose() => _ambiente.Dispose();

    private CompraCartao RecarregarCompra(CompraCartao compra)
        => _ambiente.ComprasCartao.Obter(compra.Id)!;

    private CompraCartao CadastrarNotebookDe4800Em12Vezes()
        => CadastrarCompra("Notebook", 4800.00m, quantidadeParcelas: 12);

    private CompraCartao CadastrarCompraDeUmaParcela()
        => CadastrarCompra("Livraria Central", 80.90m, quantidadeParcelas: 1);

    private CompraCartao CadastrarCompra(string descricao, decimal valorTotal, int quantidadeParcelas)
    {
        Cartao cartao = new() { Nome = "Cartão de teste", DiaVencimento = 10 };
        _ambiente.Cartoes.Salvar(cartao);

        CompraCartao compra = new()
        {
            CartaoId = cartao.Id,
            Descricao = descricao,
            ValorTotal = valorTotal,
            QuantidadeParcelas = quantidadeParcelas,
            CompetenciaPrimeiraParcela = PrimeiraParcela
        };

        _ambiente.ComprasCartao.Salvar(compra);

        return compra;
    }
}
