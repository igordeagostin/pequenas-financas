using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoResumo(
    ServicoRendas servicoRendas,
    ServicoRendasExtras servicoRendasExtras,
    ServicoGastosFixos servicoGastosFixos,
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
        IReadOnlyList<GastoFixo> gastosFixosVigentes = servicoGastosFixos.ListarVigentes(competencia);
        IReadOnlyList<ParcelaCalculada> parcelasDeCartao = servicoParcelas.ObterParcelasDeCartao(competencia);
        IReadOnlyList<ParcelaCalculada> parcelasForaDoCartao = servicoParcelas.ObterParcelasForaDoCartao(competencia);
        IReadOnlyList<SaldoReserva> saldosDeReservas = MontarSaldosDeReservas(competencia);

        return new ResumoMes
        {
            Competencia = competencia,
            EhMesEmAndamento = competencia == Competencia.DaData(hoje),
            DiasParaGastar = ContarDiasParaGastar(competencia, hoje),
            TotalRendas = rendasVigentes.Sum(renda => ServicoRecorrencia.ValorNoMes(renda, competencia)),
            TotalRendasExtras = rendasExtras.Sum(renda => renda.Valor),
            TotalGastosFixos = gastosFixosVigentes.Sum(gasto => ServicoRecorrencia.ValorNoMes(gasto, competencia)),
            TotalCartoes = parcelasDeCartao.Sum(parcela => parcela.Valor),
            TotalParcelamentos = parcelasForaDoCartao.Sum(parcela => parcela.Valor),
            TotalGuardado = saldosDeReservas.Sum(saldo => saldo.MovimentadoNoMes),
            Faturas = MontarFaturas(parcelasDeCartao),
            Lancamentos = MontarLancamentos(
                competencia, rendasVigentes, rendasExtras, gastosFixosVigentes,
                parcelasDeCartao, parcelasForaDoCartao),
            GastosPorCategoria = MontarGastosPorCategoria(
                competencia, gastosFixosVigentes, parcelasDeCartao, parcelasForaDoCartao),
            SaldosDeReservas = saldosDeReservas
        };
    }

    private static int ContarDiasParaGastar(Competencia competencia, DateTime hoje)
        => competencia == Competencia.DaData(hoje)
            ? competencia.QuantidadeDeDias - hoje.Day + 1
            : competencia.QuantidadeDeDias;

    private IReadOnlyList<FaturaCartao> MontarFaturas(IReadOnlyList<ParcelaCalculada> parcelasDeCartao)
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
        IEnumerable<GastoFixo> gastosFixos,
        IEnumerable<ParcelaCalculada> parcelasDeCartao,
        IEnumerable<ParcelaCalculada> parcelasForaDoCartao)
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
            .. gastosFixos.Select(gasto => new LancamentoDoMes
            {
                OrigemId = gasto.Id,
                Origem = OrigemLancamento.GastoFixo,
                Descricao = gasto.Descricao,
                Valor = ServicoRecorrencia.ValorNoMes(gasto, competencia),
                EhEntrada = false,
                Detalhe = "Gasto fixo",
                Categoria = gasto.Categoria,
                DiaDoMes = gasto.DiaVencimento
            }),
            .. parcelasDeCartao.Select(parcela => new LancamentoDoMes
            {
                OrigemId = parcela.OrigemId,
                Origem = OrigemLancamento.CompraCartao,
                Descricao = parcela.Descricao,
                Valor = parcela.Valor,
                EhEntrada = false,
                Detalhe = $"{parcela.NomeCartao} · parcela {parcela.Progresso}",
                Categoria = parcela.Categoria
            }),
            .. parcelasForaDoCartao.Select(parcela => new LancamentoDoMes
            {
                OrigemId = parcela.OrigemId,
                Origem = OrigemLancamento.Parcelamento,
                Descricao = parcela.Descricao,
                Valor = parcela.Valor,
                EhEntrada = false,
                Detalhe = $"Parcelado · parcela {parcela.Progresso}",
                Categoria = parcela.Categoria
            })
        ];

        return [.. lancamentos.OrderByDescending(lancamento => lancamento.EhEntrada)
                              .ThenByDescending(lancamento => lancamento.Valor)];
    }

    private static IReadOnlyList<TotalPorCategoria> MontarGastosPorCategoria(
        Competencia competencia,
        IEnumerable<GastoFixo> gastosFixos,
        IEnumerable<ParcelaCalculada> parcelasDeCartao,
        IEnumerable<ParcelaCalculada> parcelasForaDoCartao)
    {
        IEnumerable<(string Categoria, decimal Valor)> gastos =
        [
            .. gastosFixos.Select(gasto => (gasto.Categoria, ServicoRecorrencia.ValorNoMes(gasto, competencia))),
            .. parcelasDeCartao.Select(parcela => (parcela.Categoria, parcela.Valor)),
            .. parcelasForaDoCartao.Select(parcela => (parcela.Categoria, parcela.Valor))
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
