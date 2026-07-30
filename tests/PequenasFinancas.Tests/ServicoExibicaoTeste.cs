using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class ServicoExibicaoTeste : IDisposable
{
    private static readonly ResolucaoDaJanela ResolucaoEscolhida = new(1600, 900);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void SemNenhumAjusteOAppUsaAResolucaoPadrao()
    {
        PreferenciasExibicao preferencias = _ambiente.Exibicao.Preferencias;

        Assert.Equal(ResolucaoDaJanela.Padrao, preferencias.Resolucao);
        Assert.True(preferencias.LembrarUltimaResolucao);
        Assert.False(preferencias.JanelaMaximizada);
        Assert.Equal(PreferenciasExibicao.TamanhoDoTextoPadrao, preferencias.TamanhoDoTextoEmPorcentagem);
        Assert.Equal(TelaInicial.ResumoDoMes, preferencias.TelaInicial);
    }

    [Fact]
    public void UltimaResolucaoUsadaFicaGuardada()
    {
        _ambiente.Exibicao.GuardarUltimaResolucao(ResolucaoEscolhida, maximizada: false);

        Assert.Equal(ResolucaoEscolhida, _ambiente.Exibicao.Preferencias.Resolucao);
    }

    [Fact]
    public void UltimaResolucaoUsadaContinuaNaProximaAbertura()
    {
        _ambiente.Exibicao.GuardarUltimaResolucao(ResolucaoEscolhida, maximizada: false);

        BancoJson bancoReaberto = new(_ambiente.Banco.CaminhoDoArquivo);

        Assert.Equal(ResolucaoEscolhida, bancoReaberto.Dados.Exibicao.Resolucao);
    }

    [Fact]
    public void JanelaFechadaMaximizadaAbreMaximizada()
    {
        _ambiente.Exibicao.GuardarUltimaResolucao(ResolucaoEscolhida, maximizada: true);

        Assert.True(_ambiente.Exibicao.Preferencias.JanelaMaximizada);
    }

    [Fact]
    public void ComTamanhoFixoEscolhidoAUltimaResolucaoNaoEGuardada()
    {
        Salvar(preferencias =>
        {
            preferencias.LembrarUltimaResolucao = false;
            preferencias.UsarResolucao(ResolucaoDaJanela.Padrao);
        });

        _ambiente.Exibicao.GuardarUltimaResolucao(new ResolucaoDaJanela(1920, 1080), maximizada: false);

        Assert.Equal(ResolucaoDaJanela.Padrao, _ambiente.Exibicao.Preferencias.Resolucao);
    }

    [Fact]
    public void ResolucaoMenorQueOMinimoSobeParaOMinimo()
    {
        Salvar(preferencias => preferencias.UsarResolucao(new ResolucaoDaJanela(300, 200)));

        Assert.Equal(
            new ResolucaoDaJanela(ResolucaoDaJanela.LarguraMinima, ResolucaoDaJanela.AlturaMinima),
            _ambiente.Exibicao.Preferencias.Resolucao);
    }

    [Fact]
    public void TamanhoDoTextoForaDaListaEncostaNoLimite()
    {
        Salvar(preferencias => preferencias.TamanhoDoTextoEmPorcentagem = 500);

        Assert.Equal(
            PreferenciasExibicao.TamanhosDoTextoDisponiveis.Max(),
            _ambiente.Exibicao.Preferencias.TamanhoDoTextoEmPorcentagem);
    }

    [Fact]
    public void TamanhoDoTextoViraFatorDeAmpliacao()
    {
        Salvar(preferencias => preferencias.TamanhoDoTextoEmPorcentagem = 125);

        Assert.Equal(1.25, _ambiente.Exibicao.Preferencias.FatorDoTexto);
    }

    [Fact]
    public void TrocarAResolucaoAvisaAJanela()
    {
        int avisos = 0;
        _ambiente.Exibicao.ResolucaoAlterada += () => avisos++;

        Salvar(preferencias => preferencias.UsarResolucao(ResolucaoEscolhida));

        Assert.Equal(1, avisos);
    }

    [Fact]
    public void TrocarSoATelaInicialNaoMexeNoTamanhoDaJanela()
    {
        int avisos = 0;
        _ambiente.Exibicao.ResolucaoAlterada += () => avisos++;
        _ambiente.Exibicao.TamanhoDoTextoAlterado += () => avisos++;

        Salvar(preferencias => preferencias.TelaInicial = TelaInicial.ComprasNoCartao);

        Assert.Equal(0, avisos);
        Assert.Equal(TelaInicial.ComprasNoCartao, _ambiente.Exibicao.Preferencias.TelaInicial);
    }

    [Fact]
    public void TrocarOTamanhoDoTextoAvisaAJanela()
    {
        int avisos = 0;
        _ambiente.Exibicao.TamanhoDoTextoAlterado += () => avisos++;

        Salvar(preferencias => preferencias.TamanhoDoTextoEmPorcentagem = 110);

        Assert.Equal(1, avisos);
    }

    public void Dispose() => _ambiente.Dispose();

    private void Salvar(Action<PreferenciasExibicao> ajuste)
    {
        PreferenciasExibicao preferencias = Clonador.Clonar(_ambiente.Exibicao.Preferencias);

        ajuste(preferencias);

        _ambiente.Exibicao.Salvar(preferencias);
    }
}
