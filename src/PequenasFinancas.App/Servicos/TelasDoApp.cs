using PequenasFinancas.App.Componentes.Compartilhados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.App.Servicos;

public static class TelasDoApp
{
    public static IReadOnlyList<TelaDoApp> Todas { get; } =
    [
        new("Resumo do mês", "", NomeIcone.Resumo, TelaInicial.ResumoDoMes),
        new("Planejamento da semana", "semana", NomeIcone.Semana, TelaInicial.PlanejamentoDaSemana),
        new("Renda", "rendas", NomeIcone.Renda, TelaInicial.Renda),
        new("Contas e parcelas", "contas", NomeIcone.Conta, TelaInicial.ContasEParcelas),
        new("Cartões", "cartoes", NomeIcone.Cartao, TelaInicial.Cartoes),
        new("Compras no cartão", "compras", NomeIcone.Compra, TelaInicial.ComprasNoCartao),
        new("Dinheiro guardado", "reservas", NomeIcone.Guardado, TelaInicial.DinheiroGuardado),
        new("Configurações", "configuracoes", NomeIcone.Configuracoes)
    ];

    public static IReadOnlyList<TelaDoApp> QueAbremOApp { get; } = [.. Todas.Where(tela => tela.Abertura is not null)];

    public static string RotaDe(TelaInicial abertura)
        => QueAbremOApp.FirstOrDefault(tela => tela.Abertura == abertura)?.Rota ?? string.Empty;
}
