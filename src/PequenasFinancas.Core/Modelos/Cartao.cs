using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class Cartao : IRegistro, IPagavelPorMes
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;

    public string Bandeira { get; set; } = string.Empty;

    public decimal Limite { get; set; }

    public int DiaFechamento { get; set; } = 1;

    public int DiaVencimento { get; set; } = 10;

    public string Cor { get; set; } = "#0F766E";

    public bool Ativo { get; set; } = true;

    public List<Competencia> MesesPagos { get; set; } = [];
}
