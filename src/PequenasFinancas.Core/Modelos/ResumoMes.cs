using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed record ResumoMes
{
    public required Competencia Competencia { get; init; }

    public required bool EhMesEmAndamento { get; init; }

    public required int DiasParaGastar { get; init; }

    public required decimal TotalRendas { get; init; }

    public required decimal TotalRendasExtras { get; init; }

    public required decimal TotalGastosFixos { get; init; }

    public required decimal TotalCartoes { get; init; }

    public required decimal TotalParcelamentos { get; init; }

    public required decimal TotalGuardado { get; init; }

    public required decimal TotalGastosPagos { get; init; }

    public required IReadOnlyList<FaturaCartao> Faturas { get; init; }

    public required IReadOnlyList<LancamentoDoMes> Lancamentos { get; init; }

    public required IReadOnlyList<TotalPorCategoria> GastosPorCategoria { get; init; }

    public required IReadOnlyList<SaldoReserva> SaldosDeReservas { get; init; }

    public decimal TotalReceitas => TotalRendas + TotalRendasExtras;

    public decimal TotalGastos => TotalGastosFixos + TotalCartoes + TotalParcelamentos;

    public decimal TotalGastosAPagar => TotalGastos - TotalGastosPagos;

    public bool TudoPago => TotalGastos > 0 && TotalGastosAPagar <= 0;

    public decimal DinheiroLivre => TotalReceitas - TotalGastos;

    public decimal LivrePorDia
        => DiasParaGastar <= 0 || DinheiroLivre <= 0
            ? 0
            : Dinheiro.ArredondarParaBaixo(DinheiroLivre / DiasParaGastar);

    public decimal TotalGuardadoAcumulado => SaldosDeReservas.Sum(saldo => saldo.Saldo);

    public bool NoVermelho => DinheiroLivre < 0;
}

public sealed record FaturaCartao
{
    public required Guid CartaoId { get; init; }

    public required string Nome { get; init; }

    public required string Cor { get; init; }

    public required decimal Total { get; init; }

    public required int DiaVencimento { get; init; }

    public required bool EstaPaga { get; init; }

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

    public required decimal Saldo { get; init; }

    public required decimal MovimentadoNoMes { get; init; }
}
