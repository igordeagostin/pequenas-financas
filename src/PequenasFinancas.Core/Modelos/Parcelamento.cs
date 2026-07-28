using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class Parcelamento : IParcelado
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Descricao { get; set; } = string.Empty;

    public string Credor { get; set; } = string.Empty;

    public decimal ValorTotal { get; set; }

    public int QuantidadeParcelas { get; set; } = 1;

    public Competencia CompetenciaPrimeiraParcela { get; set; } = Competencia.Atual;

    public string Categoria { get; set; } = string.Empty;
}
