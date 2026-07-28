namespace PequenasFinancas.Core.Modelos;

public sealed record LancamentoDoMes
{
    public required Guid OrigemId { get; init; }

    public required OrigemLancamento Origem { get; init; }

    public required string Descricao { get; init; }

    public required decimal Valor { get; init; }

    public required bool EhEntrada { get; init; }

    public string Detalhe { get; init; } = string.Empty;

    public string Categoria { get; init; } = string.Empty;

    public bool EstaPago { get; init; }

    public int? DiaDoMes { get; init; }
}
