namespace PequenasFinancas.App.Servicos;

public static class Validacoes
{
    private const int UltimoDiaDoMes = 31;

    public static string? Descricao(string? descricao)
        => string.IsNullOrWhiteSpace(descricao) ? "Escreva um nome para identificar este lançamento." : null;

    public static string? Nome(string? nome)
        => string.IsNullOrWhiteSpace(nome) ? "Escreva um nome." : null;

    public static string? ValorPositivo(decimal valor)
        => valor <= 0 ? "Informe um valor maior que zero." : null;

    public static string? QuantidadeParcelas(int quantidade)
        => quantidade < 1 ? "O número de parcelas deve ser 1 ou mais." : null;

    public static string? DiaDoMes(int dia)
        => dia is < 1 or > UltimoDiaDoMes ? "O dia deve estar entre 1 e 31." : null;

    public static string? Selecionado(Guid id, string oQueFalta)
        => id == Guid.Empty ? $"Escolha {oQueFalta}." : null;

    public static string? Primeira(params string?[] mensagens)
        => mensagens.FirstOrDefault(mensagem => mensagem is not null);
}
