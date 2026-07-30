using System.Globalization;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PequenasFinancas.App.Servicos;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.App;

public partial class App : Application
{
    public const string ChaveDosServicos = "servicos";

    private const string CulturaDoApp = "pt-BR";

    protected override void OnStartup(StartupEventArgs argumentos)
    {
        base.OnStartup(argumentos);

        AplicarCulturaBrasileira();

        IServiceProvider servicos = ConstruirProvedorDeServicos();
        servicos.GetRequiredService<ServicoCategorias>().ImportarDosLancamentos();

        Resources.Add(ChaveDosServicos, servicos);

        new MainWindow().Show();
    }

    private static void AplicarCulturaBrasileira()
    {
        CultureInfo cultura = new(CulturaDoApp);

        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.DefaultThreadCurrentUICulture = cultura;
        CultureInfo.CurrentCulture = cultura;
        CultureInfo.CurrentUICulture = cultura;
    }

    private static IServiceProvider ConstruirProvedorDeServicos()
    {
        ServiceCollection servicos = new();

        servicos.AddWpfBlazorWebView();
#if DEBUG
        servicos.AddBlazorWebViewDeveloperTools();
#endif

        servicos.AddSingleton<BancoJson>();
        servicos.AddSingleton<ServicoCategorias>();
        servicos.AddSingleton<ServicoCartoes>();
        servicos.AddSingleton<ServicoRendas>();
        servicos.AddSingleton<ServicoRendasExtras>();
        servicos.AddSingleton<ServicoContas>();
        servicos.AddSingleton<ServicoComprasCartao>();
        servicos.AddSingleton<ServicoImportacaoFatura>();
        servicos.AddSingleton<ServicoReservas>();
        servicos.AddSingleton<ServicoPagamentos>();
        servicos.AddSingleton<ServicoParcelas>();
        servicos.AddSingleton<ServicoResumo>();
        servicos.AddSingleton<ServicoSemanas>();
        servicos.AddSingleton<EstadoAplicacao>();

        return servicos.BuildServiceProvider();
    }
}
