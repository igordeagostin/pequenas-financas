using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed record ResumoSemana
{
    public required Guid SemanaId { get; init; }

    public required Competencia Competencia { get; init; }

    public required DateTime DataInicio { get; init; }

    public required DateTime DataFim { get; init; }

    public required DateTime? DataFechamento { get; init; }

    public required int DiasDaSemana { get; init; }

    public required int DiasRestantesNoMes { get; init; }

    public required int DiasQueFaltamNaSemana { get; init; }

    public required decimal SaldoInicial { get; init; }

    public required decimal PodeGastarNaSemana { get; init; }

    public required decimal? SaldoInformado { get; init; }

    public required IReadOnlyList<GastoProvavel> GastosProvaveis { get; init; }

    public bool EstaFechada => SaldoInformado is not null;

    public decimal TotalGastosProvaveis => GastosProvaveis.Sum(gasto => gasto.Valor);

    public decimal AindaLivreNaSemana => PodeGastarNaSemana - TotalGastosProvaveis;

    public bool PassouDoPlanejado => AindaLivreNaSemana < 0;

    public decimal FicaParaORestoDoMes => SaldoInicial - PodeGastarNaSemana;

    public decimal SaldoPrevistoNoFim => SaldoInicial - TotalGastosProvaveis;

    public decimal DiferencaDoPrevisto => (SaldoInformado ?? SaldoPrevistoNoFim) - SaldoPrevistoNoFim;

    public decimal LivrePorDiaNaSemana
        => DiasQueFaltamNaSemana <= 0 || AindaLivreNaSemana <= 0
            ? 0
            : Dinheiro.ArredondarParaBaixo(AindaLivreNaSemana / DiasQueFaltamNaSemana);
}
