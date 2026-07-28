using System.Globalization;

namespace PequenasFinancas.Core.Comum;

public static class Dinheiro
{
    private const decimal CentavosNoReal = 100m;
    private const decimal Milhar = 1000m;
    private const char SeparadorDecimalBrasileiro = ',';
    private const char SeparadorDecimalInvariante = '.';

    private static readonly CultureInfo CulturaBrasileira = new("pt-BR");

    public static decimal ArredondarParaBaixo(decimal valor)
        => Math.Floor(valor * CentavosNoReal) / CentavosNoReal;

    public static bool TentarConverter(string? texto, out decimal valor)
    {
        valor = 0m;

        var textoLimpo = RemoverEspacos(texto);
        if (textoLimpo.Length == 0)
        {
            return true;
        }

        var textoInvariante = PadronizarSeparadorDecimal(textoLimpo);

        return decimal.TryParse(
            textoInvariante,
            NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture,
            out valor);
    }

    public static string FormatarParaEdicao(decimal valor)
        => valor.ToString("0.00", CulturaBrasileira);

    private static string RemoverEspacos(string? texto)
        => new(texto?.Where(caractere => !char.IsWhiteSpace(caractere)).ToArray() ?? []);

    private static string PadronizarSeparadorDecimal(string texto)
    {
        var textoSemMilhar = texto.Contains(SeparadorDecimalBrasileiro)
            ? texto.Replace(SeparadorDecimalInvariante.ToString(), string.Empty)
            : texto;

        var textoInvariante = textoSemMilhar
            .Replace(SeparadorDecimalBrasileiro, SeparadorDecimalInvariante);

        return textoInvariante.EndsWith(SeparadorDecimalInvariante)
            ? textoInvariante[..^1]
            : textoInvariante;
    }

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
            : $"{quantidadeParcelas}x de {Formatar(RateioParcelas.CalcularValorDeCadaParcela(valorTotal, quantidadeParcelas))}";
}
