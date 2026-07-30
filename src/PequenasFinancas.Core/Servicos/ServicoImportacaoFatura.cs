using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Importacao;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoImportacaoFatura(ServicoComprasCartao servicoComprasCartao)
{
    private readonly ServicoComprasCartao _servicoComprasCartao = servicoComprasCartao;

    public int Importar(IEnumerable<CompraDaFatura> comprasDaFatura, Guid cartaoId, Competencia mesDaFatura)
    {
        List<CompraCartao> comprasNovas =
            [.. comprasDaFatura
                .Where(compra => compra.Selecionada)
                .Select(compra => ConverterEmCompraDeCartao(compra, cartaoId, mesDaFatura))];

        _servicoComprasCartao.SalvarVarios(comprasNovas);

        return comprasNovas.Count;
    }

    private static CompraCartao ConverterEmCompraDeCartao(
        CompraDaFatura compraDaFatura, Guid cartaoId, Competencia mesDaFatura)
        => new()
        {
            CartaoId = cartaoId,
            Descricao = compraDaFatura.Descricao,
            ValorTotal = compraDaFatura.ValorTotal,
            QuantidadeParcelas = compraDaFatura.QuantidadeParcelas,
            CompetenciaPrimeiraParcela = compraDaFatura.CalcularCompetenciaDaPrimeiraParcela(mesDaFatura)
        };
}
