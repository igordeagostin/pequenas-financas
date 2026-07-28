using System.Globalization;

namespace PequenasFinancas.App.Servicos;

/// <summary>
/// Monta trechos de estilo para as barras. O número precisa ir com ponto decimal,
/// senão o navegador não entende a largura.
/// </summary>
public static class Estilos
{
    public static string Largura(decimal percentual)
        => $"width: {percentual.ToString(CultureInfo.InvariantCulture)}%";

    public static string Largura(decimal percentual, string cor)
        => $"{Largura(percentual)}; background: {cor}";
}
