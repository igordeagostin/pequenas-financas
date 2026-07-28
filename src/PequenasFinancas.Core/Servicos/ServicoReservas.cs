using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public sealed class ServicoReservas(BancoJson banco) : ServicoCrud<Reserva>(banco)
{
    protected override List<Reserva> Colecao => Banco.Dados.Reservas;

    public void RegistrarMovimento(Guid reservaId, MovimentoReserva movimento)
    {
        Reserva reserva = Obter(reservaId)
            ?? throw new InvalidOperationException("Reserva não encontrada.");

        int indiceExistente = reserva.Movimentos.FindIndex(existente => existente.Id == movimento.Id);

        if (indiceExistente >= 0)
        {
            reserva.Movimentos[indiceExistente] = movimento;
        }
        else
        {
            reserva.Movimentos.Add(movimento);
        }

        Banco.Salvar();
    }

    public void ExcluirMovimento(Guid reservaId, Guid movimentoId)
    {
        Reserva? reserva = Obter(reservaId);

        if (reserva is not null && reserva.Movimentos.RemoveAll(movimento => movimento.Id == movimentoId) > 0)
        {
            Banco.Salvar();
        }
    }

    public IReadOnlyList<MovimentoReserva> ListarMovimentos(Guid reservaId)
        => [.. (Obter(reservaId)?.Movimentos ?? []).OrderByDescending(movimento => movimento.Data)];

    public static decimal CalcularSaldo(Reserva reserva, Competencia ate)
        => reserva.SaldoInicial + reserva.Movimentos
            .Where(movimento => movimento.Competencia <= ate)
            .Sum(movimento => movimento.ValorComSinal);

    public static decimal CalcularMovimentadoNoMes(Reserva reserva, Competencia competencia)
        => reserva.Movimentos
            .Where(movimento => movimento.Competencia == competencia)
            .Sum(movimento => movimento.ValorComSinal);

    public decimal CalcularSaldoTotal(Competencia ate)
        => Listar().Sum(reserva => CalcularSaldo(reserva, ate));

    protected override IEnumerable<Reserva> Ordenar(IEnumerable<Reserva> itens)
        => itens.OrderBy(reserva => reserva.Nome);
}
