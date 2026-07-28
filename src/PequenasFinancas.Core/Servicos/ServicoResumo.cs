using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoResumo(
    ServicoRendas servicoRendas,
    ServicoRendasExtras servicoRendasExtras,
    ServicoContas servicoContas,
    ServicoCartoes servicoCartoes,
    ServicoParcelas servicoParcelas,
    ServicoReservas servicoReservas)
{
    private const string CategoriaPadrao = "Sem categoria";

    public ResumoMes Calcular(Competencia competencia) => Calcular(competencia, DateTime.Today);

    public ResumoMes Calcular(Competencia competencia, DateTime hoje)
    {
        IReadOnlyList<FonteRenda> rendasVigentes = servicoRendas.ListarVigentes(competencia);
        IReadOnlyList<RendaExtra> rendasExtras = servicoRendasExtras.ListarDoMes(competencia);
        IReadOnlyList<Conta> contasVigentes = servicoContas.ListarVigentes(competencia);
        ResumoContas resumoDasContas = ServicoContas.Resumir(contasVigentes, competencia);
        IReadOnlyList<ParcelaCalculada> parcelasDeCartao = servicoParcelas.ObterParcelasDeCartao(competencia);
        IReadOnlyList<SaldoReserva> saldosDeReservas = MontarSaldosDeReservas(competencia);

        return new ResumoMes
        {
            Competencia = competencia,
            EhMesEmAndamento = competencia == Competencia.DaData(hoje),
            DiasParaGastar = ContarDiasParaGastar(competencia, hoje),
            TotalRendas = rendasVigentes.Sum(renda => ServicoRecorrencia.ValorNoMes(renda, competencia)),
            TotalRendasExtras = rendasExtras.Sum(renda => renda.Valor),
            TotalContas = resumoDasContas.Total,
            TotalCartoes = parcelasDeCartao.Sum(parcela => parcela.Valor),
            TotalGuardado = saldosDeReservas.Sum(saldo => saldo.MovimentadoNoMes),
            TotalGastosPagos = resumoDasContas.Pago + SomarFaturasPagas(parcelasDeCartao),
            Faturas = MontarFaturas(competencia, parcelasDeCartao),
            Lancamentos = MontarLancamentos(
                competencia, rendasVigentes, rendasExtras, contasVigentes, parcelasDeCartao),
            GastosPorCategoria = MontarGastosPorCategoria(
                competencia, contasVigentes, parcelasDeCartao),
            SaldosDeReservas = saldosDeReservas
        };
    }

    private static int ContarDiasParaGastar(Competencia competencia, DateTime hoje)
        => competencia == Competencia.DaData(hoje)
            ? competencia.QuantidadeDeDias - hoje.Day + 1
            : competencia.QuantidadeDeDias;

    private static decimal SomarFaturasPagas(IEnumerable<ParcelaCalculada> parcelasDeCartao)
        => parcelasDeCartao.Where(parcela => parcela.EstaPago).Sum(parcela => parcela.Valor);

    private IReadOnlyList<FaturaCartao> MontarFaturas(
        Competencia competencia, IReadOnlyList<ParcelaCalculada> parcelasDeCartao)
        => [.. servicoCartoes.Listar()
            .Select(cartao => new
            {
                Cartao = cartao,
                Parcelas = parcelasDeCartao.Where(parcela => parcela.CartaoId == cartao.Id).ToList()
            })
            .Where(agrupamento => agrupamento.Parcelas.Count > 0)
            .Select(agrupamento => new FaturaCartao
            {
                CartaoId = agrupamento.Cartao.Id,
                Nome = agrupamento.Cartao.Nome,
                Cor = agrupamento.Cartao.Cor,
                DiaVencimento = agrupamento.Cartao.DiaVencimento,
                EstaPaga = ServicoPagamentos.EstaPago(agrupamento.Cartao, competencia),
                Total = agrupamento.Parcelas.Sum(parcela => parcela.Valor),
                Parcelas = agrupamento.Parcelas
            })
            .OrderByDescending(fatura => fatura.Total)];

    private IReadOnlyList<SaldoReserva> MontarSaldosDeReservas(Competencia competencia)
        => [.. servicoReservas.Listar().Select(reserva => new SaldoReserva
        {
            ReservaId = reserva.Id,
            Nome = reserva.Nome,
            Cor = reserva.Cor,
            Saldo = ServicoReservas.CalcularSaldo(reserva, competencia),
            MovimentadoNoMes = ServicoReservas.CalcularMovimentadoNoMes(reserva, competencia)
        })];

    private static IReadOnlyList<LancamentoDoMes> MontarLancamentos(
        Competencia competencia,
        IEnumerable<FonteRenda> rendas,
        IEnumerable<RendaExtra> rendasExtras,
        IEnumerable<Conta> contas,
        IEnumerable<ParcelaCalculada> parcelasDeCartao)
    {
        List<LancamentoDoMes> lancamentos =
        [
            .. rendas.Select(renda => new LancamentoDoMes
            {
                OrigemId = renda.Id,
                Origem = OrigemLancamento.Renda,
                Descricao = renda.Descricao,
                Valor = ServicoRecorrencia.ValorNoMes(renda, competencia),
                EhEntrada = true,
                Detalhe = renda.Tipo == TipoRenda.Principal ? "Renda principal" : "Renda complementar",
                DiaDoMes = renda.DiaRecebimento
            }),
            .. rendasExtras.Select(renda => new LancamentoDoMes
            {
                OrigemId = renda.Id,
                Origem = OrigemLancamento.RendaExtra,
                Descricao = renda.Descricao,
                Valor = renda.Valor,
                EhEntrada = true,
                Detalhe = "Entrada extra deste mês",
                DiaDoMes = renda.Data.Day
            }),
            .. contas.Select(conta => new LancamentoDoMes
            {
                OrigemId = conta.Id,
                Origem = OrigemLancamento.Conta,
                Descricao = conta.Descricao,
                Valor = ServicoRecorrencia.ValorNoMes(conta, competencia),
                EhEntrada = false,
                Detalhe = "Conta do mês",
                Categoria = conta.Categoria,
                DiaDoMes = conta.DiaVencimento,
                EstaPago = ServicoPagamentos.EstaPago(conta, competencia)
            }),
            .. parcelasDeCartao.Select(parcela => new LancamentoDoMes
            {
                OrigemId = parcela.OrigemId,
                Origem = OrigemLancamento.CompraCartao,
                Descricao = parcela.Descricao,
                Valor = parcela.Valor,
                EhEntrada = false,
                Detalhe = $"{parcela.NomeCartao} · parcela {parcela.Progresso}",
                Categoria = parcela.Categoria,
                EstaPago = parcela.EstaPago
            })
        ];

        return [.. lancamentos.OrderByDescending(lancamento => lancamento.EhEntrada)
                              .ThenByDescending(lancamento => lancamento.Valor)];
    }

    private static IReadOnlyList<TotalPorCategoria> MontarGastosPorCategoria(
        Competencia competencia,
        IEnumerable<Conta> contas,
        IEnumerable<ParcelaCalculada> parcelasDeCartao)
    {
        IEnumerable<(string Categoria, decimal Valor)> gastos =
        [
            .. contas.Select(conta => (conta.Categoria, ServicoRecorrencia.ValorNoMes(conta, competencia))),
            .. parcelasDeCartao.Select(parcela => (parcela.Categoria, parcela.Valor))
        ];

        return [.. gastos
            .GroupBy(gasto => string.IsNullOrWhiteSpace(gasto.Categoria) ? CategoriaPadrao : gasto.Categoria.Trim())
            .Select(grupo => new TotalPorCategoria
            {
                Categoria = grupo.Key,
                Total = grupo.Sum(gasto => gasto.Valor)
            })
            .OrderByDescending(total => total.Total)];
    }
}
