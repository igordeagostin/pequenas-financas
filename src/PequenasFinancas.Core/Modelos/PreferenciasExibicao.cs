namespace PequenasFinancas.Core.Modelos;

public sealed class PreferenciasExibicao
{
    public const int TamanhoDoTextoPadrao = 100;

    public static IReadOnlyList<int> TamanhosDoTextoDisponiveis { get; } = [90, TamanhoDoTextoPadrao, 110, 125];

    public int LarguraDaJanela { get; set; } = ResolucaoDaJanela.Padrao.Largura;

    public int AlturaDaJanela { get; set; } = ResolucaoDaJanela.Padrao.Altura;

    public bool JanelaMaximizada { get; set; }

    public bool LembrarUltimaResolucao { get; set; } = true;

    public int TamanhoDoTextoEmPorcentagem { get; set; } = TamanhoDoTextoPadrao;

    public TelaInicial TelaInicial { get; set; } = TelaInicial.ResumoDoMes;

    public ResolucaoDaJanela Resolucao => new(LarguraDaJanela, AlturaDaJanela);

    public double FatorDoTexto => (double)TamanhoDoTextoEmPorcentagem / TamanhoDoTextoPadrao;

    public void UsarResolucao(ResolucaoDaJanela resolucao)
    {
        LarguraDaJanela = resolucao.Largura;
        AlturaDaJanela = resolucao.Altura;
    }
}
