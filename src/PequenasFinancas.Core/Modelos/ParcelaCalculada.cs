using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed record ParcelaCalculada
{
    public required Guid OrigemId { get; init; }

    public required OrigemLancamento Origem { get; init; }

    public required string Descricao { get; init; }

    public required decimal Valor { get; init; }

    public required int Numero { get; init; }

    public required int QuantidadeParcelas { get; init; }

    public required decimal ValorTotal { get; init; }

    public required Competencia UltimaCompetencia { get; init; }

    public string Categoria { get; init; } = string.Empty;

    public Guid? CartaoId { get; init; }

    public string NomeCartao { get; init; } = string.Empty;

    public string Progresso => $"{Numero}/{QuantidadeParcelas}";

    public bool EhUltimaParcela => Numero == QuantidadeParcelas;
}
