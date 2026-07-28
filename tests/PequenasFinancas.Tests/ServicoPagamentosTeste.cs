using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class ServicoPagamentosTeste : IDisposable
{
    private static readonly Competencia MesAnalisado = new(2026, 7);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void ContaMarcadaComoPagaValeSoNoMesMarcado()
    {
        Conta conta = CriarConta();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);

        Assert.True(_ambiente.Pagamentos.EstaPago(TipoPagavel.Conta, conta.Id, MesAnalisado));
        Assert.False(_ambiente.Pagamentos.EstaPago(TipoPagavel.Conta, conta.Id, MesAnalisado.Proxima()));
    }

    [Fact]
    public void MarcarDuasVezesVoltaAoEstadoNaoPago()
    {
        Conta conta = CriarConta();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);
        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);

        Assert.False(_ambiente.Pagamentos.EstaPago(TipoPagavel.Conta, conta.Id, MesAnalisado));
        Assert.Empty(_ambiente.Contas.Listar().Single().MesesPagos);
    }

    [Fact]
    public void ContaPagaApareceComoPagaNoLancamentoDoMes()
    {
        Conta conta = CriarConta();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);

        LancamentoDoMes lancamento = _ambiente.Resumo.Calcular(MesAnalisado).Lancamentos.Single();

        Assert.True(lancamento.EstaPago);
    }

    [Fact]
    public void FaturaPagaMarcaTodasAsParcelasDoCartaoNoMes()
    {
        Cartao cartao = CriarCartaoComDuasCompras();

        _ambiente.Pagamentos.Alternar(TipoPagavel.FaturaCartao, cartao.Id, MesAnalisado);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.True(resumo.Faturas.Single().EstaPaga);
        Assert.All(resumo.Faturas.Single().Parcelas, parcela => Assert.True(parcela.EstaPago));
    }

    [Fact]
    public void FaturaPagaEmUmMesNaoMarcaOMesSeguinte()
    {
        Cartao cartao = CriarCartaoComDuasCompras();

        _ambiente.Pagamentos.Alternar(TipoPagavel.FaturaCartao, cartao.Id, MesAnalisado);

        Assert.False(_ambiente.Resumo.Calcular(MesAnalisado.Proxima()).Faturas.Single().EstaPaga);
    }

    [Fact]
    public void FaltaPagarDescontaSoOQueFoiMarcado()
    {
        Conta conta = CriarConta();
        CriarCarneDoSofa();
        Cartao cartao = CriarCartaoComDuasCompras();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);
        _ambiente.Pagamentos.Alternar(TipoPagavel.FaturaCartao, cartao.Id, MesAnalisado);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(2100.00m, resumo.TotalGastos);
        Assert.Equal(1900.00m, resumo.TotalGastosPagos);
        Assert.Equal(200.00m, resumo.TotalGastosAPagar);
        Assert.False(resumo.TudoPago);
    }

    [Fact]
    public void MesTodoMarcadoFicaComoTudoPago()
    {
        Conta conta = CriarConta();
        Conta carne = CriarCarneDoSofa();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);
        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, carne.Id, MesAnalisado);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.True(resumo.TudoPago);
        Assert.Equal(0m, resumo.TotalGastosAPagar);
    }

    [Fact]
    public void ResumoDoMesSeparaOQueFoiPagoDoQueFalta()
    {
        Conta conta = CriarConta();
        CriarCarneDoSofa();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);

        ResumoContas resumo = _ambiente.Contas.ResumirMes(MesAnalisado);

        Assert.Equal(1700.00m, resumo.Total);
        Assert.Equal(1500.00m, resumo.Pago);
        Assert.Equal(200.00m, resumo.APagar);
        Assert.Equal(2, resumo.Quantidade);
        Assert.Equal(1, resumo.QuantidadePaga);
        Assert.Equal(1, resumo.QuantidadeAPagar);
        Assert.False(resumo.TudoPago);
    }

    [Fact]
    public void ResumoDoMesNaoContaContaQueNaoValeNaqueleMes()
    {
        Conta conta = CriarConta();
        CriarCarneDoSofa();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);

        ResumoContas resumoDepoisDoCarne = _ambiente.Contas.ResumirMes(MesAnalisado.Adicionar(6));

        Assert.Equal(1500.00m, resumoDepoisDoCarne.Total);
        Assert.Equal(0m, resumoDepoisDoCarne.Pago);
        Assert.Equal(1500.00m, resumoDepoisDoCarne.APagar);
        Assert.Equal(1, resumoDepoisDoCarne.Quantidade);
    }

    [Fact]
    public void ResumoDoMesSemContaVigenteFicaZerado()
    {
        ResumoContas resumo = _ambiente.Contas.ResumirMes(MesAnalisado);

        Assert.Equal(0m, resumo.Total);
        Assert.Equal(0m, resumo.APagar);
        Assert.Equal(0, resumo.Quantidade);
        Assert.False(resumo.TudoPago);
    }

    [Fact]
    public void MarcarComoPagoNaoMexeNoDinheiroLivre()
    {
        _ambiente.Rendas.Salvar(new FonteRenda
        {
            Descricao = "Salário",
            Valor = 6000.00m,
            VigenciaInicio = MesAnalisado
        });

        Conta conta = CriarConta();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(4500.00m, resumo.DinheiroLivre);
        Assert.Equal(1500.00m, resumo.TotalGastos);
    }

    [Fact]
    public void MarcacaoSobreviveARecargaDoArquivo()
    {
        Conta conta = CriarConta();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, conta.Id, MesAnalisado);
        _ambiente.Banco.Carregar();

        Assert.True(_ambiente.Pagamentos.EstaPago(TipoPagavel.Conta, conta.Id, MesAnalisado));
    }

    [Fact]
    public void MarcarUmItemInexistenteNaoQuebra()
    {
        _ambiente.Pagamentos.Alternar(TipoPagavel.Conta, Guid.NewGuid(), MesAnalisado);

        Assert.False(_ambiente.Pagamentos.EstaPago(TipoPagavel.Conta, Guid.NewGuid(), MesAnalisado));
    }

    public void Dispose() => _ambiente.Dispose();

    private Conta CriarConta()
    {
        Conta conta = new()
        {
            Descricao = "Aluguel",
            Valor = 1500.00m,
            Categoria = "Moradia",
            VigenciaInicio = MesAnalisado
        };

        _ambiente.Contas.Salvar(conta);

        return conta;
    }

    private Conta CriarCarneDoSofa()
    {
        Conta carne = new()
        {
            Descricao = "Carnê do sofá",
            Valor = 200.00m,
            Categoria = "Casa",
            VigenciaInicio = MesAnalisado,
            VigenciaFim = MesAnalisado.Adicionar(5)
        };

        _ambiente.Contas.Salvar(carne);

        return carne;
    }

    private Cartao CriarCartaoComDuasCompras()
    {
        Cartao cartao = new() { Nome = "Nubank", DiaVencimento = 27 };
        _ambiente.Cartoes.Salvar(cartao);

        _ambiente.ComprasCartao.Salvar(new CompraCartao
        {
            CartaoId = cartao.Id,
            Descricao = "Notebook",
            ValorTotal = 3600.00m,
            QuantidadeParcelas = 12,
            CompetenciaPrimeiraParcela = MesAnalisado,
            Categoria = "Eletrônicos"
        });

        _ambiente.ComprasCartao.Salvar(new CompraCartao
        {
            CartaoId = cartao.Id,
            Descricao = "Fone",
            ValorTotal = 300.00m,
            QuantidadeParcelas = 3,
            CompetenciaPrimeiraParcela = MesAnalisado,
            Categoria = "Eletrônicos"
        });

        return cartao;
    }
}
