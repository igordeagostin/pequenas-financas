using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

/// <summary>Gasto pontual pago no próprio mês, sem cartão e sem parcelamento.</summary>
public sealed class GastoAvulso : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Competencia Competencia { get; set; } = Competencia.Atual;

    public DateTime Data { get; set; } = DateTime.Today;

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public string Categoria { get; set; } = string.Empty;
}
