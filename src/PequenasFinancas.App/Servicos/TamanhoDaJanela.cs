using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.App.Servicos;

public static class TamanhoDaJanela
{
    private const string CodigoDoUltimoTamanho = "ultimo";
    private const string CodigoDaTelaCheia = "tela-cheia";

    public static IReadOnlyList<OpcaoDeTamanhoDaJanela> Opcoes { get; } =
    [
        new(CodigoDoUltimoTamanho, "Lembrar o último tamanho que eu deixar"),
        new(CodigoDaTelaCheia, "Abrir em tela cheia"),
        .. ResolucaoDaJanela.Disponiveis.Select(resolucao => new OpcaoDeTamanhoDaJanela(resolucao.Codigo, Descrever(resolucao)))
    ];

    public static string CodigoDe(PreferenciasExibicao preferencias)
    {
        if (preferencias.LembrarUltimaResolucao)
        {
            return CodigoDoUltimoTamanho;
        }

        return preferencias.JanelaMaximizada ? CodigoDaTelaCheia : preferencias.Resolucao.Codigo;
    }

    public static void Aplicar(string codigo, PreferenciasExibicao preferencias)
    {
        preferencias.LembrarUltimaResolucao = codigo == CodigoDoUltimoTamanho;
        preferencias.JanelaMaximizada = codigo == CodigoDaTelaCheia;

        if (ResolucaoDaJanela.PorCodigo(codigo) is ResolucaoDaJanela escolhida)
        {
            preferencias.UsarResolucao(escolhida);
        }
    }

    public static bool GuardaOUltimoTamanho(string codigo) => codigo == CodigoDoUltimoTamanho;

    private static string Descrever(ResolucaoDaJanela resolucao)
        => resolucao == ResolucaoDaJanela.Padrao
            ? $"{resolucao.Descricao} (tamanho padrão)"
            : resolucao.Descricao;
}
