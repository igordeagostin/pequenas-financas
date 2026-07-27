using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoGastosAvulsos(BancoJson banco) : ServicoCrud<GastoAvulso>(banco)
{
    protected override List<GastoAvulso> Colecao => Banco.Dados.GastosAvulsos;

    public IReadOnlyList<GastoAvulso> ListarDoMes(Competencia competencia)
        => [.. Listar().Where(gasto => gasto.Competencia == competencia)];

    protected override IEnumerable<GastoAvulso> Ordenar(IEnumerable<GastoAvulso> itens)
        => itens.OrderByDescending(gasto => gasto.Data);
}
