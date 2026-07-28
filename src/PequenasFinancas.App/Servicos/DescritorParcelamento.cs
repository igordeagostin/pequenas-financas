using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.App.Servicos;

/// <summary>
/// Textos usados nas telas para explicar um parcelamento.
/// Serve tanto para compras no cartão quanto para parcelados fora do cartão.
/// </summary>
public static class DescritorParcelamento
{
    /// <summary>Ex.: "07/2026 → 06/2027".</summary>
    public static string Periodo(IParcelado parcelado)
        => parcelado.QuantidadeParcelas == 1
            ? parcelado.CompetenciaPrimeiraParcela.NomeCurto
            : $"{parcelado.CompetenciaPrimeiraParcela.NomeCurto} → {ServicoParcelas.CalcularUltimaCompetencia(parcelado).NomeCurto}";

    /// <summary>Ex.: "12x de R$ 400,00".</summary>
    public static string Parcelas(IParcelado parcelado)
        => Dinheiro.FormatarParcelamento(parcelado.ValorTotal, parcelado.QuantidadeParcelas);

    /// <summary>Como está esse parcelamento no mês que está sendo visto.</summary>
    public static string SituacaoNoMes(IParcelado parcelado, Competencia mes)
    {
        int numeroDaParcela = mes.DiferencaEmMesesDe(parcelado.CompetenciaPrimeiraParcela) + 1;

        if (numeroDaParcela < 1)
        {
            return $"Começa em {parcelado.CompetenciaPrimeiraParcela.NomeCurto}";
        }

        if (numeroDaParcela > parcelado.QuantidadeParcelas)
        {
            return "Já terminou";
        }

        return $"Parcela {numeroDaParcela} de {parcelado.QuantidadeParcelas}";
    }

    public static bool CaiNoMes(IParcelado parcelado, Competencia mes)
    {
        int indice = mes.DiferencaEmMesesDe(parcelado.CompetenciaPrimeiraParcela);
        return indice >= 0 && indice < parcelado.QuantidadeParcelas;
    }

    /// <summary>Quanto falta pagar contando a partir do mês visto.</summary>
    public static decimal ValorEmAberto(IParcelado parcelado, Competencia mes)
        => ServicoParcelas.CalcularValorEmAberto(parcelado, mes);
}
