using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoContas(BancoJson banco, ServicoCategorias servicoCategorias)
    : ServicoCrudCategorizado<Conta>(banco, servicoCategorias)
{
    protected override List<Conta> Colecao => Banco.Dados.Contas;

    public IReadOnlyList<Conta> ListarVigentes(Competencia competencia)
        => [.. ServicoRecorrencia.FiltrarVigentes(Listar(), competencia)];

    public ResumoContas ResumirMes(Competencia competencia) => Resumir(ListarVigentes(competencia), competencia);

    public static ResumoContas Resumir(IReadOnlyList<Conta> contasVigentes, Competencia competencia)
    {
        IReadOnlyList<Conta> contasPagas =
            [.. contasVigentes.Where(conta => ServicoPagamentos.EstaPago(conta, competencia))];

        return new ResumoContas
        {
            Total = ServicoRecorrencia.SomarNoMes(contasVigentes, competencia),
            Pago = ServicoRecorrencia.SomarNoMes(contasPagas, competencia),
            Quantidade = contasVigentes.Count,
            QuantidadePaga = contasPagas.Count
        };
    }

    protected override IEnumerable<Conta> Ordenar(IEnumerable<Conta> itens)
        => itens.OrderBy(conta => conta.DiaVencimento).ThenBy(conta => conta.Descricao);
}
