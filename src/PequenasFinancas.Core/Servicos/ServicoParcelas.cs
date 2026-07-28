using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoParcelas(
    BancoJson banco, ServicoCartoes servicoCartoes, ServicoPagamentos servicoPagamentos)
{
    private readonly BancoJson _banco = banco;
    private readonly ServicoCartoes _servicoCartoes = servicoCartoes;
    private readonly ServicoPagamentos _servicoPagamentos = servicoPagamentos;

    public IReadOnlyList<ParcelaCalculada> ObterParcelasDeCartao(Competencia competencia)
        => [.. _banco.Dados.ComprasCartao
            .Select(compra => MontarParcelaDeCartao(compra, competencia))
            .OfType<ParcelaCalculada>()
            .OrderByDescending(parcela => parcela.Valor)];

    public IReadOnlyList<ParcelaCalculada> ObterParcelasForaDoCartao(Competencia competencia)
        => [.. _banco.Dados.Parcelamentos
            .Select(parcelamento => MontarParcelaForaDoCartao(parcelamento, competencia))
            .OfType<ParcelaCalculada>()
            .OrderByDescending(parcela => parcela.Valor)];

    public IReadOnlyList<ParcelaCalculada> ObterParcelasDoCartao(Guid cartaoId, Competencia competencia)
        => [.. ObterParcelasDeCartao(competencia).Where(parcela => parcela.CartaoId == cartaoId)];

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

    public static decimal CalcularValorNoMes(IParcelado parcelado, Competencia competencia)
    {
        int indiceDaParcela = competencia.DiferencaEmMesesDe(parcelado.CompetenciaPrimeiraParcela);

        return indiceDaParcela < 0 || indiceDaParcela >= parcelado.QuantidadeParcelas
            ? 0
            : RateioParcelas.Calcular(parcelado.ValorTotal, parcelado.QuantidadeParcelas)[indiceDaParcela];
    }

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
            NomeCartao = _servicoCartoes.ObterNome(compra.CartaoId),
            EstaPago = _servicoPagamentos.EstaPago(TipoPagavel.FaturaCartao, compra.CartaoId, competencia)
        };
    }

    private static ParcelaCalculada? MontarParcelaForaDoCartao(Parcelamento parcelamento, Competencia competencia)
    {
        ParcelaCalculada? parcela = MontarParcela(parcelamento, competencia, OrigemLancamento.Parcelamento);

        return parcela is null
            ? null
            : parcela with { EstaPago = ServicoPagamentos.EstaPago(parcelamento, competencia) };
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
