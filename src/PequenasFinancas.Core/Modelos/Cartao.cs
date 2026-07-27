namespace PequenasFinancas.Core.Modelos;

/// <summary>Cartão de crédito onde as compras parceladas são lançadas.</summary>
public sealed class Cartao : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;

    public string Bandeira { get; set; } = string.Empty;

    public decimal Limite { get; set; }

    public int DiaFechamento { get; set; } = 1;

    public int DiaVencimento { get; set; } = 10;

    public string Cor { get; set; } = "#0F766E";

    public bool Ativo { get; set; } = true;
}
