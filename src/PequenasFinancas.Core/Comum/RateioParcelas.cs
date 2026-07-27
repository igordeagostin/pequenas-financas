namespace PequenasFinancas.Core.Comum;

/// <summary>
/// Divide um valor total em parcelas de centavos exatos.
/// Ponto único de verdade do cálculo — nenhuma outra parte do app divide valores por parcelas.
/// </summary>
public static class RateioParcelas
{
    private const int CasasDecimaisDoReal = 2;

    /// <summary>
    /// Retorna o valor de cada parcela. Todas iguais, exceto a última, que absorve
    /// a diferença de centavos gerada pelo arredondamento (ex.: 100,00 em 3x = 33,33 / 33,33 / 33,34).
    /// </summary>
    public static IReadOnlyList<decimal> Calcular(decimal valorTotal, int quantidadeParcelas)
    {
        if (quantidadeParcelas < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantidadeParcelas), quantidadeParcelas, "A quantidade de parcelas deve ser no mínimo 1.");
        }

        decimal valorDaParcela = Math.Round(
            valorTotal / quantidadeParcelas, CasasDecimaisDoReal, MidpointRounding.AwayFromZero);

        decimal[] parcelas = new decimal[quantidadeParcelas];

        for (int indice = 0; indice < quantidadeParcelas - 1; indice++)
        {
            parcelas[indice] = valorDaParcela;
        }

        parcelas[quantidadeParcelas - 1] = valorTotal - (valorDaParcela * (quantidadeParcelas - 1));

        return parcelas;
    }

    /// <summary>Valor da parcela de número <paramref name="numeroDaParcela"/> (contado a partir de 1).</summary>
    public static decimal CalcularParcela(decimal valorTotal, int quantidadeParcelas, int numeroDaParcela)
    {
        if (numeroDaParcela < 1 || numeroDaParcela > quantidadeParcelas)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numeroDaParcela), numeroDaParcela, "Número da parcela fora do intervalo do parcelamento.");
        }

        return Calcular(valorTotal, quantidadeParcelas)[numeroDaParcela - 1];
    }
}
