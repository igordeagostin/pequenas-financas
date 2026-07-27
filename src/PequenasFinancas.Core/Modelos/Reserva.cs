namespace PequenasFinancas.Core.Modelos;

/// <summary>Lugar onde o dinheiro guardado fica separado (reserva de emergência, viagem, troca de carro...).</summary>
public sealed class Reserva : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;

    /// <summary>Quanto se pretende juntar. Zero quando não há meta definida.</summary>
    public decimal Objetivo { get; set; }

    public string Cor { get; set; } = "#14B8A6";

    public List<MovimentoReserva> Movimentos { get; set; } = [];
}
