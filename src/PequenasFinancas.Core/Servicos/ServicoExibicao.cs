using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoExibicao(BancoJson banco)
{
    public event Action? ResolucaoAlterada;

    public event Action? TamanhoDoTextoAlterado;

    public PreferenciasExibicao Preferencias => banco.Dados.Exibicao;

    public void Salvar(PreferenciasExibicao preferencias)
    {
        PreferenciasExibicao anteriores = Preferencias;
        PreferenciasExibicao escolhidas = Ajustadas(preferencias);

        banco.Dados.Exibicao = escolhidas;
        banco.Salvar();

        Avisar(anteriores, escolhidas);
    }

    public void GuardarUltimaResolucao(ResolucaoDaJanela resolucao, bool maximizada)
    {
        if (!Preferencias.LembrarUltimaResolucao || JaEstaGuardada(resolucao, maximizada))
        {
            return;
        }

        Preferencias.UsarResolucao(resolucao.DentroDoMinimo());
        Preferencias.JanelaMaximizada = maximizada;

        banco.Salvar();
    }

    private static PreferenciasExibicao Ajustadas(PreferenciasExibicao preferencias)
    {
        PreferenciasExibicao ajustadas = Clonador.Clonar(preferencias);

        ajustadas.UsarResolucao(preferencias.Resolucao.DentroDoMinimo());
        ajustadas.TamanhoDoTextoEmPorcentagem = Math.Clamp(
            preferencias.TamanhoDoTextoEmPorcentagem,
            PreferenciasExibicao.TamanhosDoTextoDisponiveis.Min(),
            PreferenciasExibicao.TamanhosDoTextoDisponiveis.Max());

        return ajustadas;
    }

    private bool JaEstaGuardada(ResolucaoDaJanela resolucao, bool maximizada)
        => Preferencias.Resolucao == resolucao && Preferencias.JanelaMaximizada == maximizada;

    private void Avisar(PreferenciasExibicao anteriores, PreferenciasExibicao escolhidas)
    {
        if (anteriores.Resolucao != escolhidas.Resolucao
            || anteriores.JanelaMaximizada != escolhidas.JanelaMaximizada)
        {
            ResolucaoAlterada?.Invoke();
        }

        if (anteriores.TamanhoDoTextoEmPorcentagem != escolhidas.TamanhoDoTextoEmPorcentagem)
        {
            TamanhoDoTextoAlterado?.Invoke();
        }
    }
}
