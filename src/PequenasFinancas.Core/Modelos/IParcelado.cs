using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

/// <summary>
/// Compra dividida em parcelas, dentro ou fora do cartão de crédito.
/// As parcelas não são gravadas: são calculadas a partir destes dados.
/// </summary>
public interface IParcelado : IRegistro
{
    string Descricao { get; set; }

    decimal ValorTotal { get; set; }

    int QuantidadeParcelas { get; set; }

    Competencia CompetenciaPrimeiraParcela { get; set; }

    string Categoria { get; set; }
}
