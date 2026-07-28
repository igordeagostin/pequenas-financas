using System.Globalization;

namespace PequenasFinancas.Core.Comum;

public static class Dinheiro
{
    private const decimal CentavosNoReal = 100m;
    private const decimal Milhar = 1000m;

    private static readonly CultureInfo CulturaBrasileira = new("pt-BR");

    public static decimal ArredondarParaBaixo(decimal valor)
        => Math.Floor(valor * CentavosNoReal) / CentavosNoReal;

    public static string Formatar(decimal valor) => valor.ToString("C2", CulturaBrasileira);

    public static string FormatarResumido(decimal valor)
        => Math.Abs(valor) >= Milhar
            ? $"R$ {(valor / Milhar).ToString("0.#", CulturaBrasileira)} mil"
            : valor.ToString("C0", CulturaBrasileira);

    public static string FormatarComSinal(decimal valor, bool ehEntrada)
        => $"{(ehEntrada ? "+" : "−")} {Formatar(Math.Abs(valor))}";

    public static string FormatarParcelamento(decimal valorTotal, int quantidadeParcelas)
        => quantidadeParcelas <= 1
            ? Formatar(valorTotal)
            : $"{quantidadeParcelas}x de {Formatar(RateioParcelas.Calcular(valorTotal, quantidadeParcelas)[0])}";
}
