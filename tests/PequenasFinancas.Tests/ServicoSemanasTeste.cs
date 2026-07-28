using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.Tests;

public sealed class ServicoSemanasTeste : IDisposable
{
    private static readonly Competencia MesAnalisado = new(2026, 7);
    private static readonly DateTime PrimeiroDiaDaSemana = new(2026, 7, 11);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void PrimeiraSemanaComecaComODinheiroLivreDoMes()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);

        Assert.Equal(2100.00m, semana.SaldoInicial);
        Assert.Equal(new DateTime(2026, 7, 17), semana.DataFim);
        Assert.False(semana.EstaFechada);
    }

    [Fact]
    public void ODinheiroGuardadoNaoMudaOComecoDaSemana()
    {
        MontarMesComDinheiroLivre();
        GuardarNaReserva(800.00m);

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);

        Assert.Equal(2100.00m, semana.SaldoInicial);
    }

    [Fact]
    public void OSaldoInicialDaReservaNaoMudaOComecoDaSemana()
    {
        MontarMesComDinheiroLivre();

        _ambiente.Reservas.Salvar(new Reserva { Nome = "Emergência", SaldoInicial = 5000.00m });

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);

        Assert.Equal(2100.00m, semana.SaldoInicial);
    }

    [Fact]
    public void QuantoPodeGastarNaSemanaEProporcionalAosDiasQueFaltamNoMes()
    {
        MontarMesComDinheiroLivre();

        ResumoSemana resumo = AbrirEResumir(PrimeiroDiaDaSemana);

        Assert.Equal(7, resumo.DiasDaSemana);
        Assert.Equal(21, resumo.DiasRestantesNoMes);
        Assert.Equal(700.00m, resumo.PodeGastarNaSemana);
        Assert.Equal(1400.00m, resumo.FicaParaORestoDoMes);
    }

    [Fact]
    public void GastosProvaveisDiminuemOQueAindaEstaLivreNaSemana()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);
        AnotarGastoProvavel(semana, "Mercado", 300.00m, new DateTime(2026, 7, 11));
        AnotarGastoProvavel(semana, "Farmácia", 120.00m, new DateTime(2026, 7, 13));

        ResumoSemana resumo = Resumir(semana);

        Assert.Equal(420.00m, resumo.TotalGastosProvaveis);
        Assert.Equal(280.00m, resumo.AindaLivreNaSemana);
        Assert.False(resumo.PassouDoPlanejado);
    }

    [Fact]
    public void GastoProvavelExcluidoSaiDaConta()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);
        GastoProvavel gasto = AnotarGastoProvavel(semana, "Mercado", 300.00m, PrimeiroDiaDaSemana);

        _ambiente.Semanas.ExcluirGastoProvavel(semana.Id, gasto.Id);

        Assert.Equal(0m, Resumir(semana).TotalGastosProvaveis);
    }

    [Fact]
    public void GastosProvaveisAcimaDoLimiteDeixamASemanaNoVermelho()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);
        AnotarGastoProvavel(semana, "Pneus", 900.00m, PrimeiroDiaDaSemana);

        ResumoSemana resumo = Resumir(semana);

        Assert.True(resumo.PassouDoPlanejado);
        Assert.Equal(-200.00m, resumo.AindaLivreNaSemana);
        Assert.Equal(0m, resumo.LivrePorDiaNaSemana);
    }

    [Fact]
    public void OGastoPorDiaDaSemanaUsaSoOsDiasQueAindaFaltam()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);
        AnotarGastoProvavel(semana, "Mercado", 420.00m, PrimeiroDiaDaSemana);

        ResumoSemana resumo = ServicoSemanas.CalcularResumo(semana, new DateTime(2026, 7, 14));

        Assert.Equal(4, resumo.DiasQueFaltamNaSemana);
        Assert.Equal(70.00m, resumo.LivrePorDiaNaSemana);
    }

    [Fact]
    public void ProximaSemanaComecaComOSaldoInformadoNoFechamento()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada primeira = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);
        _ambiente.Semanas.Fechar(primeira.Id, 500.00m, new DateTime(2026, 7, 17));

        SemanaPlanejada segunda = _ambiente.Semanas.Abrir(MesAnalisado, new DateTime(2026, 7, 18));
        ResumoSemana resumo = Resumir(segunda);

        Assert.Equal(500.00m, segunda.SaldoInicial);
        Assert.Equal(14, resumo.DiasRestantesNoMes);
        Assert.Equal(250.00m, resumo.PodeGastarNaSemana);
    }

    [Fact]
    public void OProximoInicioSugeridoEODiaSeguinteAoFechamento()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada primeira = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);
        _ambiente.Semanas.Fechar(primeira.Id, 500.00m, new DateTime(2026, 7, 17));

        DateTime inicioSugerido = _ambiente.Semanas.SugerirInicio(MesAnalisado, new DateTime(2026, 7, 20));

        Assert.Equal(new DateTime(2026, 7, 18), inicioSugerido);
    }

    [Fact]
    public void SemanaFechadaMostraADiferencaDoQueEstavaPrevisto()
    {
        MontarMesComDinheiroLivre();

        SemanaPlanejada semana = _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);
        AnotarGastoProvavel(semana, "Mercado", 420.00m, PrimeiroDiaDaSemana);
        _ambiente.Semanas.Fechar(semana.Id, 1500.00m, new DateTime(2026, 7, 17));

        ResumoSemana resumo = Resumir(semana);

        Assert.True(resumo.EstaFechada);
        Assert.Equal(1680.00m, resumo.SaldoPrevistoNoFim);
        Assert.Equal(-180.00m, resumo.DiferencaDoPrevisto);
        Assert.Equal(0, resumo.DiasQueFaltamNaSemana);
    }

    [Fact]
    public void AUltimaSemanaDoMesFicaComTudoQueSobrou()
    {
        MontarMesComDinheiroLivre();

        ResumoSemana resumo = AbrirEResumir(new DateTime(2026, 7, 29));

        Assert.Equal(3, resumo.DiasDaSemana);
        Assert.Equal(new DateTime(2026, 7, 31), resumo.DataFim);
        Assert.Equal(2100.00m, resumo.PodeGastarNaSemana);
        Assert.Equal(0m, resumo.FicaParaORestoDoMes);
    }

    [Fact]
    public void MesNoVermelhoNaoDaLimiteParaASemana()
    {
        _ambiente.Rendas.Salvar(new FonteRenda
        {
            Descricao = "Salário",
            Valor = 1000.00m,
            VigenciaInicio = MesAnalisado
        });

        _ambiente.GastosFixos.Salvar(new GastoFixo
        {
            Descricao = "Aluguel",
            Valor = 1500.00m,
            Categoria = "Moradia",
            VigenciaInicio = MesAnalisado
        });

        ResumoSemana resumo = AbrirEResumir(PrimeiroDiaDaSemana);

        Assert.Equal(-500.00m, resumo.SaldoInicial);
        Assert.Equal(0m, resumo.PodeGastarNaSemana);
        Assert.Equal(0m, resumo.LivrePorDiaNaSemana);
    }

    [Fact]
    public void NaoDaParaTerDuasSemanasAbertasNoMesmoMes()
    {
        MontarMesComDinheiroLivre();

        _ambiente.Semanas.Abrir(MesAnalisado, PrimeiroDiaDaSemana);

        Assert.Throws<InvalidOperationException>(
            () => _ambiente.Semanas.Abrir(MesAnalisado, new DateTime(2026, 7, 20)));
    }

    [Fact]
    public void SemanaAbertaEmOutroMesNaoApareceNoMesAnalisado()
    {
        MontarMesComDinheiroLivre();

        _ambiente.Semanas.Abrir(MesAnalisado.Proxima(), new DateTime(2026, 8, 3));

        Assert.Null(_ambiente.Semanas.ObterAberta(MesAnalisado));
        Assert.NotNull(_ambiente.Semanas.ObterAberta(MesAnalisado.Proxima()));
    }

    public void Dispose() => _ambiente.Dispose();

    private void MontarMesComDinheiroLivre()
    {
        _ambiente.Rendas.Salvar(new FonteRenda
        {
            Descricao = "Salário",
            Valor = 3100.00m,
            Tipo = TipoRenda.Principal,
            VigenciaInicio = new Competencia(2026, 1)
        });

        _ambiente.GastosFixos.Salvar(new GastoFixo
        {
            Descricao = "Aluguel",
            Valor = 1000.00m,
            Categoria = "Moradia",
            VigenciaInicio = new Competencia(2026, 1)
        });
    }

    private void GuardarNaReserva(decimal valor)
    {
        Reserva reserva = new() { Nome = "Emergência" };
        _ambiente.Reservas.Salvar(reserva);

        _ambiente.Reservas.RegistrarMovimento(reserva.Id, new MovimentoReserva
        {
            Competencia = MesAnalisado,
            Tipo = TipoMovimentoReserva.Deposito,
            Valor = valor
        });
    }

    private GastoProvavel AnotarGastoProvavel(SemanaPlanejada semana, string descricao, decimal valor, DateTime data)
    {
        GastoProvavel gasto = new() { Descricao = descricao, Valor = valor, Data = data };
        _ambiente.Semanas.RegistrarGastoProvavel(semana.Id, gasto);

        return gasto;
    }

    private ResumoSemana AbrirEResumir(DateTime dataInicio)
        => Resumir(_ambiente.Semanas.Abrir(MesAnalisado, dataInicio));

    private static ResumoSemana Resumir(SemanaPlanejada semana)
        => ServicoSemanas.CalcularResumo(semana, semana.DataInicio);
}
