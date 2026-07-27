using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

/// <summary>
/// Transforma compras e parcelamentos nas parcelas que caem em cada mês.
/// Ponto único onde uma parcela é montada — compras de cartão e carnês usam a mesma regra.
/// </summary>
public sealed class ServicoParcelas(BancoJson banco, ServicoCartoes servicoCartoes)
{
    private readonly BancoJson _banco = banco;
    private readonly ServicoCartoes _servicoCartoes = servicoCartoes;

    /// <summary>Parcelas de compras no cartão que caem no mês informado.</summary>
    public IReadOnlyList<ParcelaCalculada> ObterParcelasDeCartao(Competencia competencia)
        => [.. _banco.Dados.ComprasCartao
            .Select(compra => MontarParcelaDeCartao(compra, competencia))
            .OfType<ParcelaCalculada>()
            .OrderByDescending(parcela => parcela.Valor)];

    /// <summary>Parcelas de compras feitas fora do cartão que caem no mês informado.</summary>
    public IReadOnlyList<ParcelaCalculada> ObterParcelasForaDoCartao(Competencia competencia)
        => [.. _banco.Dados.Parcelamentos
            .Select(parcelamento => MontarParcela(parcelamento, competencia, OrigemLancamento.Parcelamento))
            .OfType<ParcelaCalculada>()
            .OrderByDescending(parcela => parcela.Valor)];

    /// <summary>Parcelas de um cartão específico no mês.</summary>
    public IReadOnlyList<ParcelaCalculada> ObterParcelasDoCartao(Guid cartaoId, Competencia competencia)
        => [.. ObterParcelasDeCartao(competencia).Where(parcela => parcela.CartaoId == cartaoId)];

    /// <summary>Quanto ainda falta pagar de um parcelamento a partir do mês informado.</summary>
    public static decimal CalcularValorEmAberto(IParcelado parcelado, Competencia aPartirDe)
    {
        IReadOnlyList<decimal> parcelas = RateioParcelas.Calcular(
            parcelado.ValorTotal, parcelado.QuantidadeParcelas);

        int primeiroIndiceEmAberto = Math.Max(
            0, aPartirDe.DiferencaEmMesesDe(parcelado.CompetenciaPrimeiraParcela));

        return primeiroIndiceEmAberto >= parcelas.Count
            ? 0
            : parcelas.Skip(primeiroIndiceEmAberto).Sum();
    }

    /// <summary>Mês em que a última parcela será paga.</summary>
    public static Competencia CalcularUltimaCompetencia(IParcelado parcelado)
        => parcelado.CompetenciaPrimeiraParcela.Adicionar(parcelado.QuantidadeParcelas - 1);

    private ParcelaCalculada? MontarParcelaDeCartao(CompraCartao compra, Competencia competencia)
    {
        ParcelaCalculada? parcela = MontarParcela(compra, competencia, OrigemLancamento.CompraCartao);

        if (parcela is null)
        {
            return null;
        }

        return parcela with
        {
            CartaoId = compra.CartaoId,
            NomeCartao = _servicoCartoes.ObterNome(compra.CartaoId)
        };
    }

    private static ParcelaCalculada? MontarParcela(
        IParcelado parcelado, Competencia competencia, OrigemLancamento origem)
    {
        int indiceDaParcela = competencia.DiferencaEmMesesDe(parcelado.CompetenciaPrimeiraParcela);

        if (indiceDaParcela < 0 || indiceDaParcela >= parcelado.QuantidadeParcelas)
        {
            return null;
        }

        return new ParcelaCalculada
        {
            OrigemId = parcelado.Id,
            Origem = origem,
            Descricao = parcelado.Descricao,
            Valor = RateioParcelas.Calcular(parcelado.ValorTotal, parcelado.QuantidadeParcelas)[indiceDaParcela],
            Numero = indiceDaParcela + 1,
            QuantidadeParcelas = parcelado.QuantidadeParcelas,
            ValorTotal = parcelado.ValorTotal,
            UltimaCompetencia = CalcularUltimaCompetencia(parcelado),
            Categoria = parcelado.Categoria
        };
    }
}
