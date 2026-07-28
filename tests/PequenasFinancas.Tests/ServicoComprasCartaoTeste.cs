using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class ServicoComprasCartaoTeste : IDisposable
{
    private static readonly Competencia PrimeiraParcela = new(2026, 7);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void GastoDeCadaMesSomaAsParcelasDeTodosOsCartoes()
    {
        CadastrarNotebookDe4800Em12Vezes();
        CadastrarCelularDe1200Em3VezesAPartirDeAgosto();

        IReadOnlyList<GastoMensalCartao> gastos = CalcularGastosDeQuatroMeses(cartaoId: null);

        Assert.Equal(0m, gastos[0].Valor);
        Assert.Equal(400.00m, gastos[1].Valor);
        Assert.Equal(800.00m, gastos[2].Valor);
        Assert.Equal(800.00m, gastos[3].Valor);
    }

    [Fact]
    public void GastoFiltradoConsideraSomenteOCartaoEscolhido()
    {
        Guid cartaoDoNotebook = CadastrarNotebookDe4800Em12Vezes();
        CadastrarCelularDe1200Em3VezesAPartirDeAgosto();

        IReadOnlyList<GastoMensalCartao> gastos = CalcularGastosDeQuatroMeses(cartaoDoNotebook);

        Assert.Equal(0m, gastos[0].Valor);
        Assert.All(gastos.Skip(1), gasto => Assert.Equal(400.00m, gasto.Valor));
    }

    [Fact]
    public void PeriodoDoGraficoTrazUmPontoParaCadaMesNaOrdem()
    {
        CadastrarNotebookDe4800Em12Vezes();

        IReadOnlyList<GastoMensalCartao> gastos = CalcularGastosDeQuatroMeses(cartaoId: null);

        Assert.Equal(4, gastos.Count);
        Assert.Equal(PrimeiraParcela.Anterior(), gastos[0].Competencia);
        Assert.Equal(PrimeiraParcela.Adicionar(2), gastos[3].Competencia);
    }

    [Fact]
    public void MesDepoisDaUltimaParcelaFicaZerado()
    {
        CadastrarNotebookDe4800Em12Vezes();

        Assert.Equal(0m, _ambiente.ComprasCartao.CalcularTotalDoMes(null, PrimeiraParcela.Adicionar(12)));
    }

    [Fact]
    public void PeriodoPrecisaTerAoMenosUmMes()
        => Assert.Throws<ArgumentOutOfRangeException>(
            () => _ambiente.ComprasCartao.CalcularGastosPorMes(null, PrimeiraParcela, 0));

    public void Dispose() => _ambiente.Dispose();

    private IReadOnlyList<GastoMensalCartao> CalcularGastosDeQuatroMeses(Guid? cartaoId)
        => _ambiente.ComprasCartao.CalcularGastosPorMes(cartaoId, PrimeiraParcela.Anterior(), 4);

    private Guid CadastrarNotebookDe4800Em12Vezes()
    {
        Cartao cartao = new() { Nome = "Nubank", DiaVencimento = 27 };
        _ambiente.Cartoes.Salvar(cartao);

        _ambiente.ComprasCartao.Salvar(new CompraCartao
        {
            CartaoId = cartao.Id,
            Descricao = "Notebook",
            ValorTotal = 4800.00m,
            QuantidadeParcelas = 12,
            CompetenciaPrimeiraParcela = PrimeiraParcela
        });

        return cartao.Id;
    }

    private Guid CadastrarCelularDe1200Em3VezesAPartirDeAgosto()
    {
        Cartao cartao = new() { Nome = "Itaú", DiaVencimento = 10 };
        _ambiente.Cartoes.Salvar(cartao);

        _ambiente.ComprasCartao.Salvar(new CompraCartao
        {
            CartaoId = cartao.Id,
            Descricao = "Celular",
            ValorTotal = 1200.00m,
            QuantidadeParcelas = 3,
            CompetenciaPrimeiraParcela = PrimeiraParcela.Proxima()
        });

        return cartao.Id;
    }
}
