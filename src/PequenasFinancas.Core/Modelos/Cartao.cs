using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Modelos;

public sealed class Cartao : IRegistro, IPagavelPorMes
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;

    public int DiaVencimento { get; set; } = 10;

    public string Cor { get; set; } = "#0F766E";

    public bool Ativo { get; set; } = true;

    public List<Competencia> MesesPagos { get; set; } = [];
}
