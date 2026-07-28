using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoComprasCartao(BancoJson banco) : ServicoCrud<CompraCartao>(banco)
{
    protected override List<CompraCartao> Colecao => Banco.Dados.ComprasCartao;

    public IReadOnlyList<CompraCartao> ListarDoCartao(Guid cartaoId)
        => [.. Listar().Where(compra => compra.CartaoId == cartaoId)];

    public decimal CalcularSaldoDevedor(Guid cartaoId, Comum.Competencia competencia)
        => ListarDoCartao(cartaoId).Sum(compra => ServicoParcelas.CalcularValorEmAberto(compra, competencia));

    protected override IEnumerable<CompraCartao> Ordenar(IEnumerable<CompraCartao> itens)
        => itens.OrderByDescending(compra => compra.DataCompra).ThenBy(compra => compra.Descricao);
}
