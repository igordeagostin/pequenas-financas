using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

/// <summary>
/// Lançamento que se repete todo mês dentro de uma vigência (renda e gasto fixo).
/// </summary>
public interface IRecorrente : IRegistro
{
    string Descricao { get; set; }

    decimal Valor { get; set; }

    Competencia VigenciaInicio { get; set; }

    Competencia? VigenciaFim { get; set; }

    /// <summary>Valores que substituem o valor base em meses específicos.</summary>
    Dictionary<Competencia, decimal> Ajustes { get; set; }
}
