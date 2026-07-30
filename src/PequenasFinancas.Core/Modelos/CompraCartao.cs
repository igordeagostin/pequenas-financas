using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class CompraCartao : IParcelado
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CartaoId { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal ValorTotal { get; set; }

    public int QuantidadeParcelas { get; set; } = 1;

    public Competencia CompetenciaPrimeiraParcela { get; set; } = Competencia.Atual;

    public List<Competencia> ParcelasRemovidas { get; set; } = [];

    public string Categoria { get; set; } = string.Empty;
}
