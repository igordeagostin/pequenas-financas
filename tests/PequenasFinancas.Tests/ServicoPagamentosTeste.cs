using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class ServicoPagamentosTeste : IDisposable
{
    private static readonly Competencia MesAnalisado = new(2026, 7);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void GastoFixoMarcadoComoPagoValeSoNoMesMarcado()
    {
        GastoFixo gasto = CriarGastoFixo();

        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);

        Assert.True(_ambiente.Pagamentos.EstaPago(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado));
        Assert.False(_ambiente.Pagamentos.EstaPago(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado.Proxima()));
    }

    [Fact]
    public void MarcarDuasVezesVoltaAoEstadoNaoPago()
    {
        GastoFixo gasto = CriarGastoFixo();

        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);
        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);

        Assert.False(_ambiente.Pagamentos.EstaPago(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado));
        Assert.Empty(_ambiente.GastosFixos.Listar().Single().MesesPagos);
    }

    [Fact]
    public void GastoFixoPagoAparecePagoNoLancamentoDoMes()
    {
        GastoFixo gasto = CriarGastoFixo();

        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);

        LancamentoDoMes lancamento = _ambiente.Resumo.Calcular(MesAnalisado).Lancamentos.Single();

        Assert.True(lancamento.EstaPago);
    }

    [Fact]
    public void ParceladoPagoMarcaSomenteAParcelaDoMes()
    {
        Parcelamento parcelamento = CriarParcelamento();

        _ambiente.Pagamentos.Alternar(TipoPagavel.Parcelamento, parcelamento.Id, MesAnalisado);

        ParcelaCalculada parcelaDeJulho = _ambiente.Parcelas.ObterParcelasForaDoCartao(MesAnalisado).Single();
        ParcelaCalculada parcelaDeAgosto = _ambiente.Parcelas
            .ObterParcelasForaDoCartao(MesAnalisado.Proxima()).Single();

        Assert.True(parcelaDeJulho.EstaPago);
        Assert.False(parcelaDeAgosto.EstaPago);
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
        GastoFixo gasto = CriarGastoFixo();
        CriarParcelamento();
        Cartao cartao = CriarCartaoComDuasCompras();

        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);
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
        GastoFixo gasto = CriarGastoFixo();
        Parcelamento parcelamento = CriarParcelamento();

        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);
        _ambiente.Pagamentos.Alternar(TipoPagavel.Parcelamento, parcelamento.Id, MesAnalisado);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.True(resumo.TudoPago);
        Assert.Equal(0m, resumo.TotalGastosAPagar);
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

        GastoFixo gasto = CriarGastoFixo();

        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(4500.00m, resumo.DinheiroLivre);
        Assert.Equal(1500.00m, resumo.TotalGastos);
    }

    [Fact]
    public void MarcacaoSobreviveARecargaDoArquivo()
    {
        GastoFixo gasto = CriarGastoFixo();

        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado);
        _ambiente.Banco.Carregar();

        Assert.True(_ambiente.Pagamentos.EstaPago(TipoPagavel.GastoFixo, gasto.Id, MesAnalisado));
    }

    [Fact]
    public void MarcarUmItemInexistenteNaoQuebra()
    {
        _ambiente.Pagamentos.Alternar(TipoPagavel.GastoFixo, Guid.NewGuid(), MesAnalisado);

        Assert.False(_ambiente.Pagamentos.EstaPago(TipoPagavel.GastoFixo, Guid.NewGuid(), MesAnalisado));
    }

    public void Dispose() => _ambiente.Dispose();

    private GastoFixo CriarGastoFixo()
    {
        GastoFixo gasto = new()
        {
            Descricao = "Aluguel",
            Valor = 1500.00m,
            Categoria = "Moradia",
            VigenciaInicio = MesAnalisado
        };

        _ambiente.GastosFixos.Salvar(gasto);

        return gasto;
    }

    private Parcelamento CriarParcelamento()
    {
        Parcelamento parcelamento = new()
        {
            Descricao = "Sofá",
            Credor = "Loja de móveis",
            ValorTotal = 1200.00m,
            QuantidadeParcelas = 6,
            CompetenciaPrimeiraParcela = MesAnalisado,
            Categoria = "Casa"
        };

        _ambiente.Parcelamentos.Salvar(parcelamento);

        return parcelamento;
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
