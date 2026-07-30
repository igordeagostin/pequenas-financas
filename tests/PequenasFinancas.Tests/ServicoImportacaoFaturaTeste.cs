using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Importacao;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class ServicoImportacaoFaturaTeste : IDisposable
{
    private static readonly Competencia MesDaFatura = new(2026, 8);

    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void SomenteAsComprasMarcadasSaoImportadas()
    {
        int quantidadeImportada = Importar(
            MontarCompra("Livraria Central", 80.90m, quantidadeParcelas: 1, numeroDaParcela: 1),
            MontarCompra("Mercado Bom Preco", 142.27m, quantidadeParcelas: 1, numeroDaParcela: 1, selecionada: false));

        Assert.Equal(1, quantidadeImportada);
        Assert.Equal("Livraria Central", Assert.Single(_ambiente.ComprasCartao.Listar()).Descricao);
    }

    [Fact]
    public void CompraImportadaFicaNoCartaoEscolhido()
    {
        Guid cartaoId = CadastrarCartao();

        _ambiente.ImportacaoDeFatura.Importar(
            [MontarCompra("Livraria Central", 80.90m, quantidadeParcelas: 1, numeroDaParcela: 1)],
            cartaoId,
            MesDaFatura);

        Assert.Equal(cartaoId, Assert.Single(_ambiente.ComprasCartao.Listar()).CartaoId);
    }

    [Fact]
    public void ValorTotalEhAParcelaVezesAQuantidadeDeParcelas()
    {
        Importar(MontarCompra("Movelaria Sul", 400.00m, quantidadeParcelas: 12, numeroDaParcela: 1));

        Assert.Equal(4800.00m, Assert.Single(_ambiente.ComprasCartao.Listar()).ValorTotal);
    }

    [Fact]
    public void ParcelaJaAndadaJogaAPrimeiraParcelaParaTras()
    {
        Importar(MontarCompra("Loja do Bairro", 76.30m, quantidadeParcelas: 12, numeroDaParcela: 3));

        CompraCartao compra = Assert.Single(_ambiente.ComprasCartao.Listar());

        Assert.Equal(new Competencia(2026, 6), compra.CompetenciaPrimeiraParcela);
    }

    [Fact]
    public void CompraAVistaCaiNoMesDaFatura()
    {
        Importar(MontarCompra("Livraria Central", 80.90m, quantidadeParcelas: 1, numeroDaParcela: 1));

        Assert.Equal(MesDaFatura, Assert.Single(_ambiente.ComprasCartao.Listar()).CompetenciaPrimeiraParcela);
    }

    [Fact]
    public void FaturaSemNadaMarcadoNaoSalvaCompraNenhuma()
    {
        int quantidadeImportada = Importar(
            MontarCompra("Livraria Central", 80.90m, quantidadeParcelas: 1, numeroDaParcela: 1, selecionada: false));

        Assert.Equal(0, quantidadeImportada);
        Assert.Empty(_ambiente.ComprasCartao.Listar());
    }

    [Fact]
    public void ImportarDuasVezesCriaCompraSeparadaParaCadaImportacao()
    {
        Importar(MontarCompra("Livraria Central", 80.90m, quantidadeParcelas: 1, numeroDaParcela: 1));
        Importar(MontarCompra("Livraria Central", 80.90m, quantidadeParcelas: 1, numeroDaParcela: 1));

        Assert.Equal(2, _ambiente.ComprasCartao.Listar().Count);
    }

    public void Dispose() => _ambiente.Dispose();

    private int Importar(params CompraDaFatura[] compras)
        => _ambiente.ImportacaoDeFatura.Importar(compras, CadastrarCartao(), MesDaFatura);

    private Guid CadastrarCartao()
    {
        Cartao cartao = new() { Nome = "Cartão de teste", DiaVencimento = 9 };

        _ambiente.Cartoes.Salvar(cartao);

        return cartao.Id;
    }

    private static CompraDaFatura MontarCompra(
        string descricao,
        decimal valorDaParcela,
        int quantidadeParcelas,
        int numeroDaParcela,
        bool selecionada = true)
        => new()
        {
            Selecionada = selecionada,
            DataDaCompra = new DateTime(2026, 7, 15),
            Descricao = descricao,
            ValorDaParcela = valorDaParcela,
            QuantidadeParcelas = quantidadeParcelas,
            NumeroDaParcelaNaFatura = numeroDaParcela
        };
}
