using System.Globalization;
using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.App.Servicos;

public sealed record PontoGrafico(string Rotulo, decimal Valor, bool EhDestaque = false);

public sealed record MarcaDoGrafico(string Rotulo, decimal Valor, string X, string Y, bool EhDestaque);

public sealed record LinhaDeGrade(string Y, string Rotulo);

public sealed class TracadoGrafico
{
    private const double Largura = 760;
    private const double Altura = 250;
    private const double MargemEsquerda = 74;
    private const double MargemDireita = 18;
    private const double MargemSuperior = 16;
    private const double MargemInferior = 36;
    private const double DistanciaDoRotuloDeValor = 10;
    private const double DistanciaDoRotuloDeMes = 22;
    private const int QuantidadeDeFaixas = 4;
    private const decimal EscalaMinima = 100m;

    private TracadoGrafico(
        IReadOnlyList<MarcaDoGrafico> marcas,
        IReadOnlyList<LinhaDeGrade> linhasDeGrade,
        string linha,
        string area)
    {
        Marcas = marcas;
        LinhasDeGrade = linhasDeGrade;
        Linha = linha;
        Area = area;
    }

    public IReadOnlyList<MarcaDoGrafico> Marcas { get; }

    public IReadOnlyList<LinhaDeGrade> LinhasDeGrade { get; }

    public string Linha { get; }

    public string Area { get; }

    public static string CaixaDeDesenho => $"0 0 {Formatar(Largura)} {Formatar(Altura)}";

    public static string InicioDaGrade => Formatar(MargemEsquerda);

    public static string FimDaGrade => Formatar(Largura - MargemDireita);

    public static string FimDoRotuloDeValor => Formatar(MargemEsquerda - DistanciaDoRotuloDeValor);

    public static string BaseDosRotulosDeMes => Formatar(Altura - MargemInferior + DistanciaDoRotuloDeMes);

    public static TracadoGrafico Montar(IReadOnlyList<PontoGrafico> pontos)
    {
        decimal maiorValor = CalcularEscala(pontos);

        IReadOnlyList<MarcaDoGrafico> marcas = [.. pontos.Select(
            (ponto, indice) => new MarcaDoGrafico(
                ponto.Rotulo,
                ponto.Valor,
                Formatar(CalcularX(indice, pontos.Count)),
                Formatar(CalcularY(ponto.Valor, maiorValor)),
                ponto.EhDestaque))];

        return new TracadoGrafico(
            marcas,
            MontarLinhasDeGrade(maiorValor),
            MontarLinha(marcas),
            MontarArea(marcas));
    }

    private static decimal CalcularEscala(IReadOnlyList<PontoGrafico> pontos)
    {
        decimal maiorValor = pontos.Count == 0 ? 0 : pontos.Max(ponto => ponto.Valor);
        return maiorValor <= 0 ? EscalaMinima : maiorValor;
    }

    private static double CalcularX(int indice, int quantidadeDePontos)
    {
        double espacoUtil = Largura - MargemEsquerda - MargemDireita;

        return quantidadeDePontos <= 1
            ? MargemEsquerda + (espacoUtil / 2)
            : MargemEsquerda + (espacoUtil * indice / (quantidadeDePontos - 1));
    }

    private static double CalcularY(decimal valor, decimal maiorValor)
    {
        double alturaUtil = Altura - MargemSuperior - MargemInferior;
        return MargemSuperior + (alturaUtil * (1 - (double)(valor / maiorValor)));
    }

    private static IReadOnlyList<LinhaDeGrade> MontarLinhasDeGrade(decimal maiorValor)
        => [.. Enumerable.Range(0, QuantidadeDeFaixas + 1)
            .Select(faixa => maiorValor * faixa / QuantidadeDeFaixas)
            .Select(valor => new LinhaDeGrade(
                Formatar(CalcularY(valor, maiorValor)),
                Dinheiro.FormatarResumido(valor)))];

    private static string MontarLinha(IReadOnlyList<MarcaDoGrafico> marcas)
        => string.Join(' ', marcas.Select(marca => $"{marca.X},{marca.Y}"));

    private static string MontarArea(IReadOnlyList<MarcaDoGrafico> marcas)
    {
        if (marcas.Count == 0)
        {
            return string.Empty;
        }

        string baseDaArea = Formatar(Altura - MargemInferior);
        string caminhoDosPontos = string.Join(
            ' ', marcas.Select(marca => $"L {marca.X},{marca.Y}"));

        return $"M {marcas[0].X},{baseDaArea} {caminhoDosPontos} L {marcas[^1].X},{baseDaArea} Z";
    }

    private static string Formatar(double numero)
        => Math.Round(numero, 2).ToString(CultureInfo.InvariantCulture);
}
