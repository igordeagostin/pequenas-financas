using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class FonteRenda : IRecorrente
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public TipoRenda Tipo { get; set; } = TipoRenda.Principal;

    public int DiaRecebimento { get; set; } = 5;

    public Competencia VigenciaInicio { get; set; } = Competencia.Atual;

    public Competencia? VigenciaFim { get; set; }

    public Dictionary<Competencia, decimal> Ajustes { get; set; } = [];
}
