using Microsoft.AspNetCore.Components;
using PequenasFinancas.App.Servicos;
using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.App.Componentes.Paginas;

/// <summary>
/// Comportamento comum das telas de cadastro: listar, abrir o formulário, salvar e excluir.
/// Cada tela só declara seus campos e suas conferências.
/// </summary>
public abstract class PaginaCadastro<T> : ComponentBase, IDisposable
    where T : class, IRegistro, new()
{
    [Inject]
    protected EstadoAplicacao Estado { get; set; } = default!;

    protected abstract ServicoCrud<T> Servico { get; }

    protected IReadOnlyList<T> Itens { get; private set; } = [];

    protected T? ItemEmEdicao { get; private set; }

    protected T? ItemParaExcluir { get; private set; }

    protected string? MensagemDeErro { get; private set; }

    protected bool EhCadastroNovo { get; private set; }

    protected bool FormularioAberto => ItemEmEdicao is not null;

    protected Competencia MesSelecionado => Estado.MesSelecionado;

    protected override void OnInitialized()
    {
        Estado.MesAlterado += AoTrocarDeMes;
        Recarregar();
    }

    public void Dispose()
    {
        Estado.MesAlterado -= AoTrocarDeMes;
        GC.SuppressFinalize(this);
    }

    /// <summary>Relê os dados da tela. Telas com listas extras completam este método.</summary>
    protected virtual void Recarregar() => Itens = Servico.Listar();

    /// <summary>Item em branco usado ao clicar em "novo". Cada tela pode preencher valores iniciais.</summary>
    protected virtual T CriarNovo() => new();

    protected void AbrirNovo()
    {
        EhCadastroNovo = true;
        MensagemDeErro = null;
        ItemEmEdicao = CriarNovo();
    }

    protected void AbrirEdicao(T item)
    {
        EhCadastroNovo = false;
        MensagemDeErro = null;
        ItemEmEdicao = Clonador.Clonar(item);
    }

    protected void FecharFormulario()
    {
        ItemEmEdicao = null;
        MensagemDeErro = null;
    }

    protected void ConfirmarFormulario()
    {
        if (ItemEmEdicao is null)
        {
            return;
        }

        MensagemDeErro = Conferir(ItemEmEdicao);

        if (MensagemDeErro is not null)
        {
            return;
        }

        Servico.Salvar(ItemEmEdicao);
        ItemEmEdicao = null;
        Recarregar();
    }

    /// <summary>Devolve a mensagem de erro quando algo está faltando, ou nada quando pode salvar.</summary>
    protected abstract string? Conferir(T item);

    protected void PedirExclusao(T item) => ItemParaExcluir = item;

    protected void CancelarExclusao() => ItemParaExcluir = null;

    protected void ConfirmarExclusao()
    {
        if (ItemParaExcluir is null)
        {
            return;
        }

        Servico.Excluir(ItemParaExcluir.Id);
        ItemParaExcluir = null;
        Recarregar();
    }

    /// <summary>Nome mostrado na pergunta de confirmação da exclusão.</summary>
    protected abstract string DescreverParaExclusao(T item);

    private void AoTrocarDeMes()
    {
        Recarregar();
        InvokeAsync(StateHasChanged);
    }
}
