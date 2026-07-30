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

    public static string Previa(IParcelado parcelado)
        => parcelado.ValorTotal <= 0 || parcelado.QuantidadeParcelas < 1
            ? "Preencha o valor e o número de parcelas."
            : $"{Parcelas(parcelado)} · {Periodo(parcelado)}";

    public static int NumeroDaParcelaNoMes(IParcelado parcelado, Competencia mes)
        => mes.DiferencaEmMesesDe(parcelado.CompetenciaPrimeiraParcela) + 1;

    public static string ParcelaDoMes(IParcelado parcelado, Competencia mes)
        => $"{NumeroDaParcelaNoMes(parcelado, mes)} de {parcelado.QuantidadeParcelas}";

    public static string SituacaoNoMes(IParcelado parcelado, Competencia mes)
    {
        int numeroDaParcela = NumeroDaParcelaNoMes(parcelado, mes);

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

    public static decimal ValorNoMes(IParcelado parcelado, Competencia mes)
        => ServicoParcelas.CalcularValorNoMes(parcelado, mes);

    public static decimal ValorEmAberto(IParcelado parcelado, Competencia mes)
        => ServicoParcelas.CalcularValorEmAberto(parcelado, mes);

    public static decimal ValorQueSobrou(IParcelado parcelado)
        => ServicoParcelas.CalcularValorQueSobrou(parcelado);
}
