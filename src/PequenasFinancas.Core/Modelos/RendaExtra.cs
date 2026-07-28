using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class RendaExtra : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Competencia Competencia { get; set; } = Competencia.Atual;

    public DateTime Data { get; set; } = DateTime.Today;

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }
}
