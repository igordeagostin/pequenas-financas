using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public interface IParcelado : ICategorizavel
{
    string Descricao { get; set; }

    decimal ValorTotal { get; set; }

    int QuantidadeParcelas { get; set; }

    Competencia CompetenciaPrimeiraParcela { get; set; }
}
