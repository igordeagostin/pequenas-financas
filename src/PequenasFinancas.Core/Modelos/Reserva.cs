namespace PequenasFinancas.Core.Modelos;

public sealed class Reserva : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;

    public decimal SaldoInicial { get; set; }

    public string Cor { get; set; } = "#14B8A6";

    public List<MovimentoReserva> Movimentos { get; set; } = [];
}
