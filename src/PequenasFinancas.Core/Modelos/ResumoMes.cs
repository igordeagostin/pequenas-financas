using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

/// <summary>Retrato financeiro de um mês: o que entrou, o que saiu e quanto sobra.</summary>
public sealed record ResumoMes
{
    public required Competencia Competencia { get; init; }

    public required decimal TotalRendas { get; init; }

    public required decimal TotalRendasExtras { get; init; }

    public required decimal TotalGastosFixos { get; init; }

    public required decimal TotalCartoes { get; init; }

    public required decimal TotalParcelamentos { get; init; }

    public required decimal TotalGastosAvulsos { get; init; }

    /// <summary>Quanto foi guardado no mês (depósitos menos resgates).</summary>
    public required decimal TotalGuardado { get; init; }

    public required IReadOnlyList<FaturaCartao> Faturas { get; init; }

    public required IReadOnlyList<LancamentoDoMes> Lancamentos { get; init; }

    public required IReadOnlyList<TotalPorCategoria> GastosPorCategoria { get; init; }

    public required IReadOnlyList<SaldoReserva> SaldosDeReservas { get; init; }

    public decimal TotalReceitas => TotalRendas + TotalRendasExtras;

    public decimal TotalGastos => TotalGastosFixos + TotalCartoes + TotalParcelamentos + TotalGastosAvulsos;

    /// <summary>Quanto sobra do que entrou depois de pagar tudo, antes de separar dinheiro na reserva.</summary>
    public decimal SobraAntesDeGuardar => TotalReceitas - TotalGastos;

    /// <summary>Quanto sobra de fato, já descontado o dinheiro guardado no mês.</summary>
    public decimal SobraFinal => SobraAntesDeGuardar - TotalGuardado;

    public decimal TotalGuardadoAcumulado => SaldosDeReservas.Sum(saldo => saldo.Saldo);

    public bool NoVermelho => SobraFinal < 0;
}

/// <summary>Total das parcelas de um cartão em um mês.</summary>
public sealed record FaturaCartao
{
    public required Guid CartaoId { get; init; }

    public required string Nome { get; init; }

    public required string Cor { get; init; }

    public required decimal Total { get; init; }

    public required int DiaVencimento { get; init; }

    public required IReadOnlyList<ParcelaCalculada> Parcelas { get; init; }
}

public sealed record TotalPorCategoria
{
    public required string Categoria { get; init; }

    public required decimal Total { get; init; }
}

public sealed record SaldoReserva
{
    public required Guid ReservaId { get; init; }

    public required string Nome { get; init; }

    public required string Cor { get; init; }

    public required decimal Objetivo { get; init; }

    /// <summary>Saldo acumulado até o fim do mês consultado.</summary>
    public required decimal Saldo { get; init; }

    /// <summary>Quanto foi guardado (ou resgatado) dentro do mês consultado.</summary>
    public required decimal MovimentadoNoMes { get; init; }

    public decimal PercentualDoObjetivo
        => Objetivo <= 0 ? 0 : Math.Min(100, Math.Round(Saldo / Objetivo * 100, 1));
}
