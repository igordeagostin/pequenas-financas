using System.Text.Json.Serialization;
using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class SemanaPlanejada : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Competencia Competencia { get; set; } = Competencia.Atual;

    public DateTime DataInicio { get; set; } = DateTime.Today;

    public DateTime DataFim { get; set; } = DateTime.Today;

    public decimal SaldoInicial { get; set; }

    public decimal? SaldoInformado { get; set; }

    public DateTime? DataFechamento { get; set; }

    public List<GastoProvavel> GastosProvaveis { get; set; } = [];

    [JsonIgnore]
    public bool EstaFechada => SaldoInformado is not null;
}
