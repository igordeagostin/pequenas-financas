namespace PequenasFinancas.Core.Modelos;

/// <summary>
/// Linha única usada pela interface para listar qualquer lançamento do mês,
/// venha ele de renda, gasto fixo, parcela de cartão, parcelamento ou reserva.
/// </summary>
public sealed record LancamentoDoMes
{
    public required Guid OrigemId { get; init; }

    public required OrigemLancamento Origem { get; init; }

    public required string Descricao { get; init; }

    public required decimal Valor { get; init; }

    /// <summary>Verdadeiro para dinheiro que entra; falso para dinheiro que sai.</summary>
    public required bool EhEntrada { get; init; }

    public string Detalhe { get; init; } = string.Empty;

    public string Categoria { get; init; } = string.Empty;

    public int? DiaDoMes { get; init; }
}
