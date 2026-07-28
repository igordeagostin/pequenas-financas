using System.Text.Json.Serialization;
using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class MovimentoReserva : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Competencia Competencia { get; set; } = Competencia.Atual;

    public DateTime Data { get; set; } = DateTime.Today;

    public TipoMovimentoReserva Tipo { get; set; } = TipoMovimentoReserva.Deposito;

    public decimal Valor { get; set; }

    public string Observacao { get; set; } = string.Empty;

    [JsonIgnore]
    public decimal ValorComSinal => Tipo == TipoMovimentoReserva.Deposito ? Valor : -Valor;
}
