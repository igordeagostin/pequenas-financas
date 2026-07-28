using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoSemanas(BancoJson banco, ServicoResumo servicoResumo) : ServicoCrud<SemanaPlanejada>(banco)
{
    private const int DiasDaSemanaCheia = 7;

    protected override List<SemanaPlanejada> Colecao => Banco.Dados.Semanas;

    public IReadOnlyList<SemanaPlanejada> ListarDoMes(Competencia competencia)
        => [.. Listar().Where(semana => semana.Competencia == competencia)];

    public SemanaPlanejada? ObterAberta(Competencia competencia)
        => ListarDoMes(competencia).FirstOrDefault(semana => !semana.EstaFechada);

    public SemanaPlanejada? ObterUltimaFechada(Competencia competencia)
        => ListarDoMes(competencia).LastOrDefault(semana => semana.EstaFechada);

    public decimal CalcularSaldoParaComecar(Competencia competencia)
        => ObterUltimaFechada(competencia)?.SaldoInformado
            ?? servicoResumo.Calcular(competencia).DinheiroLivre;

    public DateTime SugerirInicio(Competencia competencia) => SugerirInicio(competencia, DateTime.Today);

    public DateTime SugerirInicio(Competencia competencia, DateTime hoje)
        => ObterUltimaFechada(competencia)?.DataFechamento is DateTime fechamento
            ? LimitarAoMes(fechamento.AddDays(1), competencia)
            : LimitarAoMes(hoje, competencia);

    public static DateTime SugerirDataDoGasto(SemanaPlanejada semana)
        => SugerirDataDoGasto(semana, DateTime.Today);

    public static DateTime SugerirDataDoGasto(SemanaPlanejada semana, DateTime hoje)
        => LimitarAoIntervalo(hoje, semana.DataInicio, semana.DataFim);

    public static DateTime SugerirDataDeFechamento(SemanaPlanejada semana)
        => SugerirDataDeFechamento(semana, DateTime.Today);

    public static DateTime SugerirDataDeFechamento(SemanaPlanejada semana, DateTime hoje)
        => LimitarAoIntervalo(hoje, semana.DataInicio, semana.Competencia.UltimoDia);

    public SemanaPlanejada Abrir(Competencia competencia, DateTime dataInicio)
    {
        if (ObterAberta(competencia) is not null)
        {
            throw new InvalidOperationException("Já existe uma semana aberta neste mês.");
        }

        DateTime inicio = LimitarAoMes(dataInicio, competencia);

        SemanaPlanejada semana = new()
        {
            Competencia = competencia,
            DataInicio = inicio,
            DataFim = LimitarAoMes(inicio.AddDays(DiasDaSemanaCheia - 1), competencia),
            SaldoInicial = CalcularSaldoParaComecar(competencia)
        };

        Salvar(semana);

        return semana;
    }

    public void Fechar(Guid semanaId, decimal saldoInformado, DateTime dataFechamento)
    {
        SemanaPlanejada semana = ObterOuFalhar(semanaId);

        semana.SaldoInformado = saldoInformado;
        semana.DataFechamento = dataFechamento.Date;

        Banco.Salvar();
    }

    public void RegistrarGastoProvavel(Guid semanaId, GastoProvavel gasto)
    {
        SemanaPlanejada semana = ObterOuFalhar(semanaId);

        int indiceExistente = semana.GastosProvaveis.FindIndex(existente => existente.Id == gasto.Id);

        if (indiceExistente >= 0)
        {
            semana.GastosProvaveis[indiceExistente] = gasto;
        }
        else
        {
            semana.GastosProvaveis.Add(gasto);
        }

        Banco.Salvar();
    }

    public void ExcluirGastoProvavel(Guid semanaId, Guid gastoId)
    {
        SemanaPlanejada? semana = Obter(semanaId);

        if (semana is not null && semana.GastosProvaveis.RemoveAll(gasto => gasto.Id == gastoId) > 0)
        {
            Banco.Salvar();
        }
    }

    public static ResumoSemana CalcularResumo(SemanaPlanejada semana) => CalcularResumo(semana, DateTime.Today);

    public static ResumoSemana CalcularResumo(SemanaPlanejada semana, DateTime hoje)
    {
        int diasDaSemana = ContarDias(semana.DataInicio, semana.DataFim);
        int diasRestantesNoMes = ContarDias(semana.DataInicio, semana.Competencia.UltimoDia);

        return new ResumoSemana
        {
            SemanaId = semana.Id,
            Competencia = semana.Competencia,
            DataInicio = semana.DataInicio,
            DataFim = semana.DataFim,
            DataFechamento = semana.DataFechamento,
            DiasDaSemana = diasDaSemana,
            DiasRestantesNoMes = diasRestantesNoMes,
            DiasQueFaltamNaSemana = ContarDiasQueFaltam(semana, hoje),
            SaldoInicial = semana.SaldoInicial,
            PodeGastarNaSemana = CalcularQuantoPodeGastar(semana.SaldoInicial, diasDaSemana, diasRestantesNoMes),
            SaldoInformado = semana.SaldoInformado,
            GastosProvaveis = [.. semana.GastosProvaveis.OrderBy(gasto => gasto.Data)]
        };
    }

    protected override IEnumerable<SemanaPlanejada> Ordenar(IEnumerable<SemanaPlanejada> itens)
        => itens.OrderBy(semana => semana.DataInicio);

    private static decimal CalcularQuantoPodeGastar(decimal saldoInicial, int diasDaSemana, int diasRestantesNoMes)
        => saldoInicial <= 0 || diasRestantesNoMes <= 0
            ? 0
            : Dinheiro.ArredondarParaBaixo(saldoInicial / diasRestantesNoMes * diasDaSemana);

    private static int ContarDiasQueFaltam(SemanaPlanejada semana, DateTime hoje)
    {
        if (semana.EstaFechada || hoje.Date > semana.DataFim.Date)
        {
            return 0;
        }

        return hoje.Date <= semana.DataInicio.Date
            ? ContarDias(semana.DataInicio, semana.DataFim)
            : ContarDias(hoje, semana.DataFim);
    }

    private static int ContarDias(DateTime inicio, DateTime fim) => (fim.Date - inicio.Date).Days + 1;

    private static DateTime LimitarAoMes(DateTime data, Competencia competencia)
        => LimitarAoIntervalo(data, competencia.PrimeiroDia, competencia.UltimoDia);

    private static DateTime LimitarAoIntervalo(DateTime data, DateTime inicio, DateTime fim)
    {
        if (data.Date < inicio.Date)
        {
            return inicio.Date;
        }

        return data.Date > fim.Date ? fim.Date : data.Date;
    }

    private SemanaPlanejada ObterOuFalhar(Guid semanaId)
        => Obter(semanaId) ?? throw new InvalidOperationException("Semana não encontrada.");
}
