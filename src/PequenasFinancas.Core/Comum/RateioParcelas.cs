namespace PequenasFinancas.Core.Comum;

public static class RateioParcelas
{
    private const int CasasDecimaisDoReal = 2;

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
