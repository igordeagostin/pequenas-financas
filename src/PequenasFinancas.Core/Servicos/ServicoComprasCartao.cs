using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoComprasCartao(BancoJson banco) : ServicoCrud<CompraCartao>(banco)
{
    protected override List<CompraCartao> Colecao => Banco.Dados.ComprasCartao;

    public IReadOnlyList<CompraCartao> ListarDoCartao(Guid cartaoId)
        => [.. Listar().Where(compra => compra.CartaoId == cartaoId)];

    public decimal CalcularSaldoDevedor(Guid cartaoId, Competencia competencia)
        => ListarDoCartao(cartaoId).Sum(compra => ServicoParcelas.CalcularValorEmAberto(compra, competencia));

    public decimal CalcularTotalDoMes(Guid? cartaoId, Competencia competencia)
        => ListarDoFiltro(cartaoId).Sum(compra => ServicoParcelas.CalcularValorNoMes(compra, competencia));

    public IReadOnlyList<GastoMensalCartao> CalcularGastosPorMes(
        Guid? cartaoId, Competencia mesInicial, int quantidadeDeMeses)
    {
        if (quantidadeDeMeses < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantidadeDeMeses), quantidadeDeMeses, "O período do gráfico precisa ter ao menos um mês.");
        }

        return [.. Enumerable.Range(0, quantidadeDeMeses)
            .Select(mesInicial.Adicionar)
            .Select(mes => new GastoMensalCartao(mes, CalcularTotalDoMes(cartaoId, mes)))];
    }

    protected override IEnumerable<CompraCartao> Ordenar(IEnumerable<CompraCartao> itens)
        => itens
            .OrderByDescending(compra => compra.CompetenciaPrimeiraParcela)
            .ThenBy(compra => compra.Descricao);

    private IReadOnlyList<CompraCartao> ListarDoFiltro(Guid? cartaoId)
        => cartaoId is null ? Listar() : ListarDoCartao(cartaoId.Value);
}
