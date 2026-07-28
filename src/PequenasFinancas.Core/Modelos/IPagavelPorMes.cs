using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public interface IPagavelPorMes : IRegistro
{
    List<Competencia> MesesPagos { get; set; }
}
