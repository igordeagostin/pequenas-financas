using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.App.Servicos;

public static class DescritorParcelamento
{
    public static string Periodo(IParcelado parcelado)
        => parcelado.QuantidadeParcelas == 1
            ? parcelado.CompetenciaPrimeiraParcela.NomeCurto
            : $"{parcelado.CompetenciaPrimeiraParcela.NomeCurto} → {ServicoParcelas.CalcularUltimaCompetencia(parcelado).NomeCurto}";

    public static string Parcelas(IParcelado parcelado)
        => Dinheiro.FormatarParcelamento(parcelado.ValorTotal, parcelado.QuantidadeParcelas);

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

    public static decimal ValorEmAberto(IParcelado parcelado, Competencia mes)
        => ServicoParcelas.CalcularValorEmAberto(parcelado, mes);
}
