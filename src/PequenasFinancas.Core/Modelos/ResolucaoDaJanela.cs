namespace PequenasFinancas.Core.Modelos;

public sealed record ResolucaoDaJanela(int Largura, int Altura)
{
    public const int LarguraMinima = 1040;
    public const int AlturaMinima = 700;

    public static ResolucaoDaJanela Padrao { get; } = new(1320, 860);

    public static IReadOnlyList<ResolucaoDaJanela> Disponiveis { get; } =
    [
        new(LarguraMinima, AlturaMinima),
        new(1280, 800),
        Padrao,
        new(1366, 768),
        new(1600, 900),
        new(1920, 1080)
    ];

    public string Codigo => $"{Largura}x{Altura}";

    public string Descricao => $"{Largura} × {Altura}";

    public static ResolucaoDaJanela? PorCodigo(string? codigo)
        => Disponiveis.FirstOrDefault(resolucao => resolucao.Codigo == codigo);

    public ResolucaoDaJanela DentroDoMinimo()
        => new(Math.Max(Largura, LarguraMinima), Math.Max(Altura, AlturaMinima));
}
