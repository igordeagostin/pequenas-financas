using System.Globalization;

namespace PequenasFinancas.Core.Comum;

/// <summary>Formatação de valores em real. Usada por toda a interface.</summary>
public static class Dinheiro
{
    private static readonly CultureInfo CulturaBrasileira = new("pt-BR");

    /// <summary>Ex.: "R$ 1.234,56".</summary>
    public static string Formatar(decimal valor) => valor.ToString("C2", CulturaBrasileira);

    /// <summary>Ex.: "+ R$ 1.234,56" ou "− R$ 1.234,56".</summary>
    public static string FormatarComSinal(decimal valor, bool ehEntrada)
        => $"{(ehEntrada ? "+" : "−")} {Formatar(Math.Abs(valor))}";

    /// <summary>Ex.: "12x de R$ 400,00".</summary>
    public static string FormatarParcelamento(decimal valorTotal, int quantidadeParcelas)
        => quantidadeParcelas <= 1
            ? Formatar(valorTotal)
            : $"{quantidadeParcelas}x de {Formatar(RateioParcelas.Calcular(valorTotal, quantidadeParcelas)[0])}";
}
