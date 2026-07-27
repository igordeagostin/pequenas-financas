namespace PequenasFinancas.Core.Modelos;

/// <summary>De onde veio cada linha exibida no resumo do mês.</summary>
public enum OrigemLancamento
{
    Renda,
    RendaExtra,
    GastoFixo,
    CompraCartao,
    Parcelamento,
    GastoAvulso,
    Reserva
}
