using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

/// <summary>Parcelamentos feitos fora do cartão de crédito.</summary>
public sealed class ServicoParcelamentos(BancoJson banco) : ServicoCrud<Parcelamento>(banco)
{
    protected override List<Parcelamento> Colecao => Banco.Dados.Parcelamentos;

    protected override IEnumerable<Parcelamento> Ordenar(IEnumerable<Parcelamento> itens)
        => itens.OrderBy(parcelamento => parcelamento.CompetenciaPrimeiraParcela)
                .ThenBy(parcelamento => parcelamento.Descricao);
}
