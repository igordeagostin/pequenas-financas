using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Importacao;

public sealed class CompraDaFatura
{
    public bool Selecionada { get; set; } = true;

    public DateTime DataDaCompra { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal ValorDaParcela { get; set; }

    public int QuantidadeParcelas { get; set; } = 1;

    public int NumeroDaParcelaNaFatura { get; set; } = 1;

    public bool EhParcelada => QuantidadeParcelas > 1;

    public decimal ValorTotal => RateioParcelas.CalcularValorTotal(ValorDaParcela, QuantidadeParcelas);

    public Competencia CalcularCompetenciaDaPrimeiraParcela(Competencia mesDaFatura)
        => mesDaFatura.Adicionar(1 - NumeroDaParcelaNaFatura);
}
