using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoRendasExtras(BancoJson banco) : ServicoCrud<RendaExtra>(banco)
{
    protected override List<RendaExtra> Colecao => Banco.Dados.RendasExtras;

    public IReadOnlyList<RendaExtra> ListarDoMes(Competencia competencia)
        => [.. Listar().Where(renda => renda.Competencia == competencia)];

    protected override IEnumerable<RendaExtra> Ordenar(IEnumerable<RendaExtra> itens)
        => itens.OrderByDescending(renda => renda.Data);
}
