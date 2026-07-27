using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

/// <summary>
/// Regras de vigência e de valor mensal dos lançamentos recorrentes.
/// Renda e gasto fixo compartilham exatamente este cálculo.
/// </summary>
public static class ServicoRecorrencia
{
    public static bool EstaVigenteEm(IRecorrente recorrente, Competencia competencia)
        => competencia.EstaEntre(recorrente.VigenciaInicio, recorrente.VigenciaFim);

    /// <summary>Valor do mês: o ajuste daquele mês quando existir, senão o valor base.</summary>
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
