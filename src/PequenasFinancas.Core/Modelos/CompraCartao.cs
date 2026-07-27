using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

/// <summary>Compra feita no cartão de crédito, à vista (1x) ou parcelada.</summary>
public sealed class CompraCartao : IParcelado
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CartaoId { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal ValorTotal { get; set; }

    public DateTime DataCompra { get; set; } = DateTime.Today;

    public int QuantidadeParcelas { get; set; } = 1;

    public Competencia CompetenciaPrimeiraParcela { get; set; } = Competencia.Atual;

    public string Categoria { get; set; } = string.Empty;
}
