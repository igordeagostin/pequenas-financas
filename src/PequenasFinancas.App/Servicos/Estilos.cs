using System.Globalization;

namespace PequenasFinancas.App.Servicos;

public static class Estilos
{
    public static string Largura(decimal percentual)
        => $"width: {percentual.ToString(CultureInfo.InvariantCulture)}%";

    public static string Largura(decimal percentual, string cor)
        => $"{Largura(percentual)}; background: {cor}";
}
