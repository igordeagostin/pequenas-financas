using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.Tests;

public sealed class AmbienteDeTeste : IDisposable
{
    private readonly string _pastaTemporaria;

    public AmbienteDeTeste()
    {
        _pastaTemporaria = Path.Combine(Path.GetTempPath(), "PequenasFinancas.Testes", Guid.NewGuid().ToString("N"));

        Banco = new BancoJson(Path.Combine(_pastaTemporaria, "dados.json"));
        Cartoes = new ServicoCartoes(Banco);
        Rendas = new ServicoRendas(Banco);
        RendasExtras = new ServicoRendasExtras(Banco);
        GastosFixos = new ServicoGastosFixos(Banco);
        ComprasCartao = new ServicoComprasCartao(Banco);
        Parcelamentos = new ServicoParcelamentos(Banco);
        Reservas = new ServicoReservas(Banco);
        Parcelas = new ServicoParcelas(Banco, Cartoes);
        Resumo = new ServicoResumo(
            Rendas, RendasExtras, GastosFixos, Cartoes, Parcelas, Reservas);
    }

    public BancoJson Banco { get; }

    public ServicoCartoes Cartoes { get; }

    public ServicoRendas Rendas { get; }

    public ServicoRendasExtras RendasExtras { get; }

    public ServicoGastosFixos GastosFixos { get; }

    public ServicoComprasCartao ComprasCartao { get; }

    public ServicoParcelamentos Parcelamentos { get; }

    public ServicoReservas Reservas { get; }

    public ServicoParcelas Parcelas { get; }

    public ServicoResumo Resumo { get; }

    public void Dispose()
    {
        if (Directory.Exists(_pastaTemporaria))
        {
            Directory.Delete(_pastaTemporaria, recursive: true);
        }
    }
}
