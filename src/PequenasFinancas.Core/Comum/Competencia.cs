using System.Globalization;

namespace PequenasFinancas.Core.Comum;

/// <summary>
/// Mês/ano de referência dos lançamentos. É a unidade pela qual o app é dividido.
/// </summary>
public readonly record struct Competencia : IComparable<Competencia>
{
    private const int PrimeiroMes = 1;
    private const int UltimoMes = 12;
    private const int MesesNoAno = 12;

    private static readonly CultureInfo CulturaBrasileira = new("pt-BR");

    public Competencia(int ano, int mes)
    {
        if (ano < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(ano), ano, "Ano inválido.");
        }

        if (mes is < PrimeiroMes or > UltimoMes)
        {
            throw new ArgumentOutOfRangeException(nameof(mes), mes, "Mês deve estar entre 1 e 12.");
        }

        Ano = ano;
        Mes = mes;
    }

    public int Ano { get; }

    public int Mes { get; }

    /// <summary>Competência do mês corrente — usada para abrir o app sempre no mês atual.</summary>
    public static Competencia Atual => DaData(DateTime.Today);

    public static Competencia DaData(DateTime data) => new(data.Year, data.Month);

    public DateTime PrimeiroDia => new(Ano, Mes, 1);

    public DateTime UltimoDia => new(Ano, Mes, DateTime.DaysInMonth(Ano, Mes));

    /// <summary>Ex.: "Julho / 2026".</summary>
    public string NomeExtenso
    {
        get
        {
            string nomeDoMes = CulturaBrasileira.DateTimeFormat.GetMonthName(Mes);
            return $"{CulturaBrasileira.TextInfo.ToTitleCase(nomeDoMes)} / {Ano}";
        }
    }

    /// <summary>Ex.: "07/2026".</summary>
    public string NomeCurto => $"{Mes:D2}/{Ano}";

    public Competencia Adicionar(int meses)
    {
        int totalDeMeses = (Ano * MesesNoAno) + (Mes - 1) + meses;
        return new Competencia(totalDeMeses / MesesNoAno, (totalDeMeses % MesesNoAno) + 1);
    }

    public Competencia Proxima() => Adicionar(1);

    public Competencia Anterior() => Adicionar(-1);

    /// <summary>Quantos meses esta competência está à frente de <paramref name="referencia"/>.</summary>
    public int DiferencaEmMesesDe(Competencia referencia)
        => ((Ano - referencia.Ano) * MesesNoAno) + (Mes - referencia.Mes);

    public bool EstaEntre(Competencia inicio, Competencia? fim)
        => this >= inicio && (fim is null || this <= fim.Value);

    public static Competencia Analisar(string texto)
        => TentarAnalisar(texto, out Competencia competencia)
            ? competencia
            : throw new FormatException($"Competência inválida: '{texto}'. Formato esperado: aaaa-MM.");

    public static bool TentarAnalisar(string? texto, out Competencia competencia)
    {
        competencia = default;

        if (string.IsNullOrWhiteSpace(texto))
        {
            return false;
        }

        string[] partes = texto.Split('-');

        if (partes.Length != 2
            || !int.TryParse(partes[0], NumberStyles.None, CultureInfo.InvariantCulture, out int ano)
            || !int.TryParse(partes[1], NumberStyles.None, CultureInfo.InvariantCulture, out int mes)
            || ano < 1
            || mes is < PrimeiroMes or > UltimoMes)
        {
            return false;
        }

        competencia = new Competencia(ano, mes);
        return true;
    }

    public int CompareTo(Competencia outra)
    {
        int comparacaoDeAno = Ano.CompareTo(outra.Ano);
        return comparacaoDeAno != 0 ? comparacaoDeAno : Mes.CompareTo(outra.Mes);
    }

    public override string ToString() => $"{Ano:D4}-{Mes:D2}";

    public static bool operator <(Competencia esquerda, Competencia direita) => esquerda.CompareTo(direita) < 0;

    public static bool operator >(Competencia esquerda, Competencia direita) => esquerda.CompareTo(direita) > 0;

    public static bool operator <=(Competencia esquerda, Competencia direita) => esquerda.CompareTo(direita) <= 0;

    public static bool operator >=(Competencia esquerda, Competencia direita) => esquerda.CompareTo(direita) >= 0;
}
