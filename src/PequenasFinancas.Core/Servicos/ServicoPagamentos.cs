using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoPagamentos(BancoJson banco)
{
    private readonly BancoJson _banco = banco;

    public static bool EstaPago(IPagavelPorMes pagavel, Competencia competencia)
        => pagavel.MesesPagos.Contains(competencia);

    public bool EstaPago(TipoPagavel tipo, Guid itemId, Competencia competencia)
        => Obter(tipo, itemId) is IPagavelPorMes pagavel && EstaPago(pagavel, competencia);

    public void Alternar(TipoPagavel tipo, Guid itemId, Competencia competencia)
    {
        if (Obter(tipo, itemId) is not IPagavelPorMes pagavel)
        {
            return;
        }

        if (!pagavel.MesesPagos.Remove(competencia))
        {
            pagavel.MesesPagos.Add(competencia);
        }

        _banco.Salvar();
    }

    private IPagavelPorMes? Obter(TipoPagavel tipo, Guid itemId)
        => ListarPagaveis(tipo).FirstOrDefault(pagavel => pagavel.Id == itemId);

    private IEnumerable<IPagavelPorMes> ListarPagaveis(TipoPagavel tipo)
        => tipo switch
        {
            TipoPagavel.GastoFixo => _banco.Dados.GastosFixos,
            TipoPagavel.Parcelamento => _banco.Dados.Parcelamentos,
            TipoPagavel.FaturaCartao => _banco.Dados.Cartoes,
            _ => []
        };
}
