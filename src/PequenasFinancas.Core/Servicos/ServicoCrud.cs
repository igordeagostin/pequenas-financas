using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public abstract class ServicoCrud<T>(BancoJson banco)
    where T : class, IRegistro
{
    protected BancoJson Banco { get; } = banco;

    protected abstract List<T> Colecao { get; }

    public IReadOnlyList<T> Listar() => [.. Ordenar(Colecao)];

    public T? Obter(Guid id) => Colecao.FirstOrDefault(item => item.Id == id);

    public void Salvar(T item)
    {
        AntesDeSalvar(item);

        int indiceExistente = Colecao.FindIndex(existente => existente.Id == item.Id);

        if (indiceExistente >= 0)
        {
            Colecao[indiceExistente] = item;
        }
        else
        {
            Colecao.Add(item);
        }

        Banco.Salvar();
    }

    public void Excluir(Guid id)
    {
        if (Obter(id) is not T item)
        {
            return;
        }

        AntesDeExcluir(item);
        Colecao.Remove(item);
        Banco.Salvar();
    }

    protected virtual void AntesDeSalvar(T item)
    {
    }

    protected virtual void AntesDeExcluir(T item)
    {
    }

    protected virtual IEnumerable<T> Ordenar(IEnumerable<T> itens) => itens;
}
