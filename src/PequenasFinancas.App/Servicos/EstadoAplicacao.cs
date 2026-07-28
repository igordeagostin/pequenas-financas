using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.App.Servicos;

/// <summary>
/// Guarda o mês que está sendo visto. O app sempre começa no mês atual.
/// </summary>
public sealed class EstadoAplicacao
{
    public Competencia MesSelecionado { get; private set; } = Competencia.Atual;

    /// <summary>Avisa as telas quando o mês muda.</summary>
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
