using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public interface IRecorrente : IRegistro
{
    string Descricao { get; set; }

    decimal Valor { get; set; }

    Competencia VigenciaInicio { get; set; }

    Competencia? VigenciaFim { get; set; }

    Dictionary<Competencia, decimal> Ajustes { get; set; }
}
