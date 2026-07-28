using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class Conta : IRecorrente, IPagavelPorMes
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public string Categoria { get; set; } = string.Empty;

    public int DiaVencimento { get; set; } = 10;

    public Competencia VigenciaInicio { get; set; } = Competencia.Atual;

    public Competencia? VigenciaFim { get; set; }

    public Dictionary<Competencia, decimal> Ajustes { get; set; } = [];

    public List<Competencia> MesesPagos { get; set; } = [];
}
