using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

/// <summary>
/// Parcela que cai em um mês. É derivada da compra ou do parcelamento — nunca é gravada no banco.
/// </summary>
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

    /// <summary>Preenchido apenas quando a parcela é de uma compra no cartão.</summary>
    public Guid? CartaoId { get; init; }

    public string NomeCartao { get; init; } = string.Empty;

    /// <summary>Ex.: "3/12".</summary>
    public string Progresso => $"{Numero}/{QuantidadeParcelas}";

    public bool EhUltimaParcela => Numero == QuantidadeParcelas;
}
