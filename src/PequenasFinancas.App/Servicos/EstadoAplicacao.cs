using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.App.Servicos;

public sealed class EstadoAplicacao
{
    public Competencia MesSelecionado { get; private set; } = Competencia.Atual;

    public event Action? MesAlterado;

    public bool EstaNoMesAtual => MesSelecionado == Competencia.Atual;

    public void Selecionar(Competencia competencia)
    {
        if (MesSelecionado == competencia)
        {
            return;
        }

        MesSelecionado = competencia;
        MesAlterado?.Invoke();
    }

    public void AvancarUmMes() => Selecionar(MesSelecionado.Proxima());

    public void VoltarUmMes() => Selecionar(MesSelecionado.Anterior());

    public void IrParaOMesAtual() => Selecionar(Competencia.Atual);
}
