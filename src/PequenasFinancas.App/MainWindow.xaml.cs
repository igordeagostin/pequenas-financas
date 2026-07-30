using System.ComponentModel;
using System.Windows;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.App;

public partial class MainWindow : Window
{
    private readonly ServicoExibicao _servicoDeExibicao;

    public MainWindow(ServicoExibicao servicoDeExibicao)
    {
        _servicoDeExibicao = servicoDeExibicao;

        InitializeComponent();

        MinWidth = ResolucaoDaJanela.LarguraMinima;
        MinHeight = ResolucaoDaJanela.AlturaMinima;

        AplicarResolucao();

        visualizador.BlazorWebViewInitialized += (_, _) => AplicarTamanhoDoTexto();
        _servicoDeExibicao.ResolucaoAlterada += AplicarResolucao;
        _servicoDeExibicao.TamanhoDoTextoAlterado += AplicarTamanhoDoTexto;
    }

    private PreferenciasExibicao Preferencias => _servicoDeExibicao.Preferencias;

    private void AoFechar(object remetente, CancelEventArgs argumentos)
    {
        _servicoDeExibicao.ResolucaoAlterada -= AplicarResolucao;
        _servicoDeExibicao.TamanhoDoTextoAlterado -= AplicarTamanhoDoTexto;

        GuardarUltimaResolucao();
    }

    private void AplicarResolucao()
    {
        Size tamanho = CaberNaAreaDeTrabalho(Preferencias.Resolucao);

        Width = tamanho.Width;
        Height = tamanho.Height;
        WindowState = Preferencias.JanelaMaximizada ? WindowState.Maximized : WindowState.Normal;
    }

    private void AplicarTamanhoDoTexto()
    {
        if (visualizador.WebView.CoreWebView2 is null)
        {
            return;
        }

        visualizador.WebView.ZoomFactor = Preferencias.FatorDoTexto;
    }

    private void GuardarUltimaResolucao()
    {
        Rect area = WindowState == WindowState.Normal
            ? new Rect(Left, Top, ActualWidth, ActualHeight)
            : RestoreBounds;

        _servicoDeExibicao.GuardarUltimaResolucao(
            new ResolucaoDaJanela((int)area.Width, (int)area.Height),
            WindowState == WindowState.Maximized);
    }

    private static Size CaberNaAreaDeTrabalho(ResolucaoDaJanela resolucao)
    {
        Rect areaDisponivel = SystemParameters.WorkArea;

        return new Size(
            Math.Min(resolucao.Largura, areaDisponivel.Width),
            Math.Min(resolucao.Altura, areaDisponivel.Height));
    }
}
