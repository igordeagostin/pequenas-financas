using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoCartoes(BancoJson banco) : ServicoCrud<Cartao>(banco)
{
    protected override List<Cartao> Colecao => Banco.Dados.Cartoes;

    public IReadOnlyList<Cartao> ListarAtivos()
        => [.. Listar().Where(cartao => cartao.Ativo)];

    public string ObterNome(Guid cartaoId)
        => Obter(cartaoId)?.Nome ?? "Cartão removido";

    protected override IEnumerable<Cartao> Ordenar(IEnumerable<Cartao> itens)
        => itens.OrderByDescending(cartao => cartao.Ativo).ThenBy(cartao => cartao.Nome);
}
