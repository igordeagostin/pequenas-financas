using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.Tests;

public sealed class ServicoResumoTeste : IDisposable
{
    private static readonly Competencia MesAnalisado = new(2026, 7);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void DinheiroLivreDescontaContasECartao()
    {
        MontarMesCompleto();

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(6000.00m, resumo.TotalReceitas);
        Assert.Equal(1700.00m, resumo.TotalContas);
        Assert.Equal(400.00m, resumo.TotalCartoes);
        Assert.Equal(3900.00m, resumo.DinheiroLivre);
    }

    [Fact]
    public void DinheiroGuardadoNoMesNaoMexeNoDinheiroLivre()
    {
        MontarMesCompleto();
        GuardarNaReserva(500.00m);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(500.00m, resumo.TotalGuardado);
        Assert.Equal(3900.00m, resumo.DinheiroLivre);
        Assert.False(resumo.NoVermelho);
    }

    [Fact]
    public void NoMesEmAndamentoOGastoPorDiaUsaSoOsDiasQueFaltam()
    {
        MontarMesCompleto();

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado, new DateTime(2026, 7, 27));

        Assert.True(resumo.EhMesEmAndamento);
        Assert.Equal(5, resumo.DiasParaGastar);
        Assert.Equal(780.00m, resumo.LivrePorDia);
    }

    [Fact]
    public void EmOutroMesOGastoPorDiaUsaTodosOsDiasDoMes()
    {
        MontarMesCompleto();

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado, new DateTime(2026, 6, 15));

        Assert.False(resumo.EhMesEmAndamento);
        Assert.Equal(31, resumo.DiasParaGastar);
        Assert.Equal(125.80m, resumo.LivrePorDia);
    }

    [Fact]
    public void DinheiroGuardadoNoMesNaoMexeNoGastoPorDia()
    {
        MontarMesCompleto();
        GuardarNaReserva(500.00m);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado, new DateTime(2026, 7, 27));

        Assert.Equal(3900.00m, resumo.DinheiroLivre);
        Assert.Equal(780.00m, resumo.LivrePorDia);
    }

    [Fact]
    public void RendaExtraDoMesEntraNasReceitas()
    {
        MontarMesCompleto();

        _ambiente.RendasExtras.Salvar(new RendaExtra
        {
            Competencia = MesAnalisado,
            Descricao = "Freela",
            Valor = 800.00m
        });

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(800.00m, resumo.TotalRendasExtras);
        Assert.Equal(6800.00m, resumo.TotalReceitas);
        Assert.Equal(4700.00m, resumo.DinheiroLivre);
    }

    [Fact]
    public void FaturaAgrupaAsParcelasPorCartao()
    {
        MontarMesCompleto();

        FaturaCartao fatura = _ambiente.Resumo.Calcular(MesAnalisado).Faturas.Single();

        Assert.Equal("Nubank", fatura.Nome);
        Assert.Equal(400.00m, fatura.Total);
        Assert.Equal("1/12", fatura.Parcelas.Single().Progresso);
    }

    [Fact]
    public void SaldoDaReservaAcumulaAoLongoDosMeses()
    {
        MontarMesCompleto();
        GuardarNaReserva(500.00m);
        GuardarNaReserva(300.00m, MesAnalisado.Proxima());

        SaldoReserva saldoEmJulho = _ambiente.Resumo.Calcular(MesAnalisado).SaldosDeReservas.Single();
        SaldoReserva saldoEmAgosto = _ambiente.Resumo.Calcular(MesAnalisado.Proxima()).SaldosDeReservas.Single();

        Assert.Equal(500.00m, saldoEmJulho.Saldo);
        Assert.Equal(800.00m, saldoEmAgosto.Saldo);
        Assert.Equal(300.00m, saldoEmAgosto.MovimentadoNoMes);
    }

    [Fact]
    public void SaldoInicialEntraNoGuardadoSemContarComoMovimentoDoMes()
    {
        MontarMesCompleto();

        Reserva reserva = new() { Nome = "Emergência", SaldoInicial = 2000.00m };
        _ambiente.Reservas.Salvar(reserva);

        GuardarNaReserva(500.00m);

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(2500.00m, resumo.SaldosDeReservas.Single().Saldo);
        Assert.Equal(500.00m, resumo.TotalGuardado);
    }

    [Fact]
    public void ResgateDiminuiOSaldoGuardado()
    {
        MontarMesCompleto();
        GuardarNaReserva(500.00m);

        Reserva reserva = _ambiente.Reservas.Listar().Single();

        _ambiente.Reservas.RegistrarMovimento(reserva.Id, new MovimentoReserva
        {
            Competencia = MesAnalisado,
            Tipo = TipoMovimentoReserva.Resgate,
            Valor = 200.00m
        });

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(300.00m, resumo.TotalGuardado);
        Assert.Equal(300.00m, resumo.SaldosDeReservas.Single().Saldo);
    }

    [Fact]
    public void GastosSaoAgrupadosPorCategoria()
    {
        MontarMesCompleto();

        IReadOnlyList<TotalPorCategoria> categorias = _ambiente.Resumo.Calcular(MesAnalisado).GastosPorCategoria;

        Assert.Equal(1500.00m, categorias.Single(categoria => categoria.Categoria == "Moradia").Total);
        Assert.Equal(400.00m, categorias.Single(categoria => categoria.Categoria == "Eletrônicos").Total);
        Assert.Equal(200.00m, categorias.Single(categoria => categoria.Categoria == "Casa").Total);
        Assert.Equal("Moradia", categorias[0].Categoria);
    }

    [Fact]
    public void MesSemNenhumLancamentoNaoQuebraOResumo()
    {
        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(0m, resumo.TotalReceitas);
        Assert.Equal(0m, resumo.TotalGastos);
        Assert.Equal(0m, resumo.DinheiroLivre);
        Assert.Equal(0m, resumo.LivrePorDia);
        Assert.Empty(resumo.Lancamentos);
        Assert.Empty(resumo.Faturas);
    }

    [Fact]
    public void ListaDeLancamentosMostraEntradasESaidasDoMes()
    {
        MontarMesCompleto();

        IReadOnlyList<LancamentoDoMes> lancamentos = _ambiente.Resumo.Calcular(MesAnalisado).Lancamentos;

        Assert.Equal(4, lancamentos.Count);
        Assert.Single(lancamentos, lancamento => lancamento.EhEntrada);
        Assert.Contains(lancamentos, lancamento => lancamento.Detalhe == "Nubank · parcela 1/12");
        Assert.Equal(2, lancamentos.Count(lancamento => lancamento.Origem == OrigemLancamento.Conta));
    }

    [Fact]
    public void GastosMaioresQueAsReceitasDeixamOMesNoVermelho()
    {
        _ambiente.Rendas.Salvar(new FonteRenda
        {
            Descricao = "Salário",
            Valor = 1000.00m,
            VigenciaInicio = MesAnalisado
        });

        _ambiente.Contas.Salvar(new Conta
        {
            Descricao = "Aluguel",
            Valor = 1500.00m,
            Categoria = "Moradia",
            VigenciaInicio = MesAnalisado
        });

        ResumoMes resumo = _ambiente.Resumo.Calcular(MesAnalisado);

        Assert.Equal(-500.00m, resumo.DinheiroLivre);
        Assert.True(resumo.NoVermelho);
        Assert.Equal(0m, resumo.LivrePorDia);
    }

    public void Dispose() => _ambiente.Dispose();

    private void MontarMesCompleto()
    {
        _ambiente.Rendas.Salvar(new FonteRenda
        {
            Descricao = "Salário",
            Valor = 6000.00m,
            Tipo = TipoRenda.Principal,
            VigenciaInicio = new Competencia(2026, 1)
        });

        _ambiente.Contas.Salvar(new Conta
        {
            Descricao = "Aluguel",
            Valor = 1500.00m,
            Categoria = "Moradia",
            VigenciaInicio = new Competencia(2026, 1)
        });

        Cartao cartao = new() { Nome = "Nubank", DiaVencimento = 27 };
        _ambiente.Cartoes.Salvar(cartao);

        _ambiente.ComprasCartao.Salvar(new CompraCartao
        {
            CartaoId = cartao.Id,
            Descricao = "Notebook",
            ValorTotal = 4800.00m,
            QuantidadeParcelas = 12,
            CompetenciaPrimeiraParcela = MesAnalisado,
            Categoria = "Eletrônicos"
        });

        _ambiente.Contas.Salvar(new Conta
        {
            Descricao = "Carnê do sofá",
            Valor = 200.00m,
            Categoria = "Casa",
            VigenciaInicio = MesAnalisado,
            VigenciaFim = MesAnalisado.Adicionar(5)
        });
    }

    private void GuardarNaReserva(decimal valor, Competencia? competencia = null)
    {
        Reserva reserva = _ambiente.Reservas.Listar().FirstOrDefault()
            ?? CriarReservaDeEmergencia();

        _ambiente.Reservas.RegistrarMovimento(reserva.Id, new MovimentoReserva
        {
            Competencia = competencia ?? MesAnalisado,
            Tipo = TipoMovimentoReserva.Deposito,
            Valor = valor
        });
    }

    private Reserva CriarReservaDeEmergencia()
    {
        Reserva reserva = new() { Nome = "Emergência" };
        _ambiente.Reservas.Salvar(reserva);

        return reserva;
    }
}
