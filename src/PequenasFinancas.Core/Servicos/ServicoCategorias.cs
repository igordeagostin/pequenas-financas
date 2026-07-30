using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoCategorias(BancoJson banco) : ServicoCrud<Categoria>(banco)
{
    protected override List<Categoria> Colecao => Banco.Dados.Categorias;

    public IReadOnlyList<string> ListarNomes() => [.. Listar().Select(categoria => categoria.Nome)];

    public Categoria? ObterPorNome(string? nome)
        => Colecao.FirstOrDefault(categoria => EhMesmoNome(categoria.Nome, nome));

    public bool ExisteOutraComOMesmoNome(Categoria categoria)
        => Colecao.Any(existente => existente.Id != categoria.Id && EhMesmoNome(existente.Nome, categoria.Nome));

    public int ContarUsos(string nome)
        => ListarCategorizaveis().Count(lancamento => EhMesmoNome(lancamento.Categoria, nome));

    public string RegistrarNome(string? nomeDigitado)
    {
        string nome = (nomeDigitado ?? string.Empty).Trim();

        if (nome.Length == 0)
        {
            return string.Empty;
        }

        if (ObterPorNome(nome) is Categoria conhecida)
        {
            return conhecida.Nome;
        }

        Colecao.Add(new Categoria { Nome = nome });

        return nome;
    }

    public void ImportarDosLancamentos()
    {
        int quantidadeInicial = Colecao.Count;
        bool algumLancamentoMudou = false;

        foreach (ICategorizavel lancamento in ListarCategorizaveis())
        {
            string nome = RegistrarNome(lancamento.Categoria);

            if (nome == lancamento.Categoria)
            {
                continue;
            }

            lancamento.Categoria = nome;
            algumLancamentoMudou = true;
        }

        if (algumLancamentoMudou || Colecao.Count > quantidadeInicial)
        {
            Banco.Salvar();
        }
    }

    protected override void AntesDeSalvar(Categoria categoria)
    {
        string nomeAnterior = Obter(categoria.Id)?.Nome ?? string.Empty;

        categoria.Nome = categoria.Nome.Trim();

        SubstituirNosLancamentos(nomeAnterior, categoria.Nome);
    }

    protected override void AntesDeExcluir(Categoria categoria)
        => SubstituirNosLancamentos(categoria.Nome, string.Empty);

    protected override IEnumerable<Categoria> Ordenar(IEnumerable<Categoria> itens)
        => itens.OrderBy(categoria => categoria.Nome, StringComparer.CurrentCultureIgnoreCase);

    private void SubstituirNosLancamentos(string nomeAnterior, string novoNome)
    {
        if (nomeAnterior.Length == 0 || nomeAnterior == novoNome)
        {
            return;
        }

        IEnumerable<ICategorizavel> lancamentosDaCategoria = ListarCategorizaveis()
            .Where(lancamento => EhMesmoNome(lancamento.Categoria, nomeAnterior));

        foreach (ICategorizavel lancamento in lancamentosDaCategoria)
        {
            lancamento.Categoria = novoNome;
        }
    }

    private IEnumerable<ICategorizavel> ListarCategorizaveis()
        => [.. Banco.Dados.Contas, .. Banco.Dados.ComprasCartao];

    private static bool EhMesmoNome(string? nome, string? outroNome)
        => string.Equals(nome?.Trim(), outroNome?.Trim(), StringComparison.CurrentCultureIgnoreCase);
}
