using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoGastosFixos(BancoJson banco) : ServicoCrud<GastoFixo>(banco)
{
    protected override List<GastoFixo> Colecao => Banco.Dados.GastosFixos;

    public IReadOnlyList<GastoFixo> ListarVigentes(Competencia competencia)
        => [.. ServicoRecorrencia.FiltrarVigentes(Listar(), competencia)];

    protected override IEnumerable<GastoFixo> Ordenar(IEnumerable<GastoFixo> itens)
        => itens.OrderBy(gasto => gasto.DiaVencimento).ThenBy(gasto => gasto.Descricao);
}
