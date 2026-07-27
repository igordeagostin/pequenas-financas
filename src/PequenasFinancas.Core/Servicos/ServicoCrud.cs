using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

/// <summary>
/// Cadastro básico compartilhado por todos os serviços: listar, obter, salvar e excluir.
/// Evita repetir a mesma manipulação de lista em cada tela.
/// </summary>
public abstract class ServicoCrud<T>(BancoJson banco)
    where T : class, IRegistro
{
    protected BancoJson Banco { get; } = banco;

    /// <summary>Coleção correspondente dentro do arquivo JSON.</summary>
    protected abstract List<T> Colecao { get; }

    public IReadOnlyList<T> Listar() => [.. Ordenar(Colecao)];

    public T? Obter(Guid id) => Colecao.FirstOrDefault(item => item.Id == id);

    /// <summary>Insere quando o item é novo e substitui quando já existe.</summary>
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

    /// <summary>Ordem em que os itens aparecem nas listas da interface.</summary>
    protected virtual IEnumerable<T> Ordenar(IEnumerable<T> itens) => itens;
}
