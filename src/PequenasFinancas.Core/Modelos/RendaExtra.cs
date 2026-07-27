using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

/// <summary>Dinheiro que entrou só naquele mês (freelance, 13º, venda, presente).</summary>
public sealed class RendaExtra : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Competencia Competencia { get; set; } = Competencia.Atual;

    public DateTime Data { get; set; } = DateTime.Today;

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }
}
