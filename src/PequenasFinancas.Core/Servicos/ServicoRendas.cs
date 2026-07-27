using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoRendas(BancoJson banco) : ServicoCrud<FonteRenda>(banco)
{
    protected override List<FonteRenda> Colecao => Banco.Dados.Rendas;

    public IReadOnlyList<FonteRenda> ListarVigentes(Competencia competencia)
        => [.. ServicoRecorrencia.FiltrarVigentes(Listar(), competencia)];

    protected override IEnumerable<FonteRenda> Ordenar(IEnumerable<FonteRenda> itens)
        => itens.OrderBy(renda => renda.Tipo).ThenBy(renda => renda.Descricao);
}
