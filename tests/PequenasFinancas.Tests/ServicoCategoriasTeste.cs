using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class ServicoCategoriasTeste : IDisposable
{
    private static readonly Competencia MesAnalisado = new(2026, 7);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void ContaComCategoriaNovaGuardaACategoria()
    {
        SalvarConta("Moradia");

        Assert.Equal(["Moradia"], _ambiente.Categorias.ListarNomes());
    }

    [Fact]
    public void CompraNoCartaoComCategoriaNovaGuardaACategoria()
    {
        SalvarCompra("Eletrônicos");

        Assert.Equal(["Eletrônicos"], _ambiente.Categorias.ListarNomes());
    }

    [Fact]
    public void CategoriaRepetidaNaoEntraDuasVezes()
    {
        SalvarConta("Moradia");
        SalvarCompra("Moradia");

        Assert.Single(_ambiente.Categorias.ListarNomes());
    }

    [Fact]
    public void CategoriaEscritaComOutraCaixaAproveitaONomeJaCadastrado()
    {
        SalvarConta("Moradia");
        Conta segundaConta = SalvarConta("moradia");

        Assert.Single(_ambiente.Categorias.ListarNomes());
        Assert.Equal("Moradia", _ambiente.Contas.Obter(segundaConta.Id)?.Categoria);
    }

    [Fact]
    public void EspacosSobrandoSaemDaCategoria()
    {
        Conta conta = SalvarConta("  Moradia  ");

        Assert.Equal(["Moradia"], _ambiente.Categorias.ListarNomes());
        Assert.Equal("Moradia", _ambiente.Contas.Obter(conta.Id)?.Categoria);
    }

    [Fact]
    public void ContaSemCategoriaNaoCadastraNada()
    {
        SalvarConta(string.Empty);

        Assert.Empty(_ambiente.Categorias.ListarNomes());
    }

    [Fact]
    public void CategoriasFicamEmOrdemAlfabetica()
    {
        SalvarConta("Transporte");
        SalvarConta("Alimentação");
        SalvarConta("Moradia");

        Assert.Equal(["Alimentação", "Moradia", "Transporte"], _ambiente.Categorias.ListarNomes());
    }

    [Fact]
    public void RenomearCategoriaTrocaONomeNosLancamentos()
    {
        Conta conta = SalvarConta("Moradia");
        CompraCartao compra = SalvarCompra("Moradia");

        Renomear("Moradia", "Casa");

        Assert.Equal(["Casa"], _ambiente.Categorias.ListarNomes());
        Assert.Equal("Casa", _ambiente.Contas.Obter(conta.Id)?.Categoria);
        Assert.Equal("Casa", _ambiente.ComprasCartao.Obter(compra.Id)?.Categoria);
    }

    [Fact]
    public void RenomearNaoMexeEmLancamentoDeOutraCategoria()
    {
        SalvarConta("Moradia");
        Conta transporte = SalvarConta("Transporte");

        Renomear("Moradia", "Casa");

        Assert.Equal("Transporte", _ambiente.Contas.Obter(transporte.Id)?.Categoria);
    }

    [Fact]
    public void ExcluirCategoriaDeixaOsLancamentosSemCategoria()
    {
        Conta conta = SalvarConta("Moradia");
        CompraCartao compra = SalvarCompra("Moradia");

        _ambiente.Categorias.Excluir(_ambiente.Categorias.ObterPorNome("Moradia")!.Id);

        Assert.Empty(_ambiente.Categorias.ListarNomes());
        Assert.Equal(string.Empty, _ambiente.Contas.Obter(conta.Id)?.Categoria);
        Assert.Equal(string.Empty, _ambiente.ComprasCartao.Obter(compra.Id)?.Categoria);
    }

    [Fact]
    public void ContagemDeUsosSomaContasECompras()
    {
        SalvarConta("Moradia");
        SalvarCompra("Moradia");
        SalvarConta("Transporte");

        Assert.Equal(2, _ambiente.Categorias.ContarUsos("Moradia"));
        Assert.Equal(1, _ambiente.Categorias.ContarUsos("Transporte"));
    }

    [Fact]
    public void CategoriaCadastradaPelaTelaFicaDisponivelSemLancamento()
    {
        _ambiente.Categorias.Salvar(new Categoria { Nome = "Lazer" });

        Assert.Equal(["Lazer"], _ambiente.Categorias.ListarNomes());
        Assert.Equal(0, _ambiente.Categorias.ContarUsos("Lazer"));
    }

    [Fact]
    public void CategoriasSobrevivemARecargaDoArquivo()
    {
        SalvarConta("Moradia");

        _ambiente.Banco.Carregar();

        Assert.Equal(["Moradia"], _ambiente.Categorias.ListarNomes());
    }

    public void Dispose() => _ambiente.Dispose();

    private void Renomear(string nomeAtual, string novoNome)
    {
        Categoria categoria = Clonador.Clonar(_ambiente.Categorias.ObterPorNome(nomeAtual)!);
        categoria.Nome = novoNome;

        _ambiente.Categorias.Salvar(categoria);
    }

    private Conta SalvarConta(string categoria)
    {
        Conta conta = new()
        {
            Descricao = "Aluguel",
            Valor = 1500.00m,
            Categoria = categoria,
            VigenciaInicio = MesAnalisado
        };

        _ambiente.Contas.Salvar(conta);

        return conta;
    }

    private CompraCartao SalvarCompra(string categoria)
    {
        Cartao cartao = new() { Nome = "Nubank", DiaVencimento = 27 };
        _ambiente.Cartoes.Salvar(cartao);

        CompraCartao compra = new()
        {
            CartaoId = cartao.Id,
            Descricao = "Notebook",
            ValorTotal = 4800.00m,
            QuantidadeParcelas = 12,
            CompetenciaPrimeiraParcela = MesAnalisado,
            Categoria = categoria
        };

        _ambiente.ComprasCartao.Salvar(compra);

        return compra;
    }
}
