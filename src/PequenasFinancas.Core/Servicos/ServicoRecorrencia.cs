using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public static class ServicoRecorrencia
{
    public static bool EstaVigenteEm(IRecorrente recorrente, Competencia competencia)
        => competencia.EstaEntre(recorrente.VigenciaInicio, recorrente.VigenciaFim);

    public static decimal ValorNoMes(IRecorrente recorrente, Competencia competencia)
        => recorrente.Ajustes.TryGetValue(competencia, out decimal valorAjustado)
            ? valorAjustado
            : recorrente.Valor;

    public static IEnumerable<T> FiltrarVigentes<T>(IEnumerable<T> recorrentes, Competencia competencia)
        where T : IRecorrente
        => recorrentes.Where(recorrente => EstaVigenteEm(recorrente, competencia));

    public static decimal SomarNoMes<T>(IEnumerable<T> recorrentes, Competencia competencia)
        where T : IRecorrente
        => FiltrarVigentes(recorrentes, competencia).Sum(recorrente => ValorNoMes(recorrente, competencia));
}
