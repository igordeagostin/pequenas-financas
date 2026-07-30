namespace PequenasFinancas.App.Servicos;

public sealed class OrdenacaoDaTabela<T> : IOrdenacaoDaTabela
{
    private readonly Dictionary<string, Func<T, IComparable?>> _chavesPorColuna = [];

    private Action? _redesenharATela;

    public string ColunaAtual { get; private set; } = string.Empty;

    public bool Crescente { get; private set; } = true;

    public OrdenacaoDaTabela<T> Coluna(string nome, Func<T, IComparable?> chave)
    {
        _chavesPorColuna[nome] = chave;

        return this;
    }

    public OrdenacaoDaTabela<T> RedesenharCom(Action redesenharATela)
    {
        _redesenharATela = redesenharATela;

        return this;
    }

    public bool EstaOrdenandoPor(string nome) => ColunaAtual == nome;

    public void Alternar(string nome)
    {
        if (!_chavesPorColuna.ContainsKey(nome))
        {
            return;
        }

        Crescente = !EstaOrdenandoPor(nome) || !Crescente;
        ColunaAtual = nome;

        _redesenharATela?.Invoke();
    }

    public IReadOnlyList<T> Aplicar(IEnumerable<T> itens)
    {
        if (!_chavesPorColuna.TryGetValue(ColunaAtual, out Func<T, IComparable?>? chave))
        {
            return [.. itens];
        }

        return Crescente
            ? [.. itens.OrderBy(chave, ComparadorDeChave.Padrao)]
            : [.. itens.OrderByDescending(chave, ComparadorDeChave.Padrao)];
    }

    private sealed class ComparadorDeChave : IComparer<IComparable?>
    {
        public static ComparadorDeChave Padrao { get; } = new();

        public int Compare(IComparable? chave, IComparable? outraChave)
        {
            if (chave is null)
            {
                return outraChave is null ? 0 : -1;
            }

            return outraChave is null ? 1 : chave.CompareTo(outraChave);
        }
    }
}
