namespace PequenasFinancas.Core.Modelos;

public sealed class GastoProvavel : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime Data { get; set; } = DateTime.Today;
}
