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
        if (Colecao.RemoveAll(item => item.Id == id) > 0)
        {
            Banco.Salvar();
        }
    }

    protected virtual IEnumerable<T> Ordenar(IEnumerable<T> itens) => itens;
}
