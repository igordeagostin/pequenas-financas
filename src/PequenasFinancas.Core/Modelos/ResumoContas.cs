namespace PequenasFinancas.Core.Modelos;

public sealed record ResumoContas
{
    public required decimal Total { get; init; }

    public required decimal Pago { get; init; }

    public required int Quantidade { get; init; }

    public required int QuantidadePaga { get; init; }

    public decimal APagar => Total - Pago;

    public int QuantidadeAPagar => Quantidade - QuantidadePaga;

    public bool TudoPago => Quantidade > 0 && QuantidadeAPagar == 0;
}
