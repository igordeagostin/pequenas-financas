using System.Globalization;
using System.Text;
using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Importacao;

public static class LeitorFaturaNubank
{
    public const string MensagemDeArquivoInvalido =
        "Este arquivo não parece ser o CSV da fatura do Nubank. "
        + "Baixe a fatura no aplicativo do Nubank e escolha o arquivo terminado em .csv.";

    private const string CabecalhoDoNubank = "date,title,amount";
    private const string MarcadorDeParcela = " - Parcela ";
    private const string FormatoDaData = "yyyy-MM-dd";
    private const char SeparadorDeColunas = ',';
    private const char Aspas = '"';
    private const char SinalDeNegativo = '-';
    private const char BarraDaParcela = '/';
    private const char SeparadorDoNomeDoArquivo = '_';
    private const char MarcaDeOrdemDeBytes = '\uFEFF';
    private const char FimDeLinha = '\n';
    private const char RetornoDeCarro = '\r';
    private const int ColunaDaData = 0;
    private const int ColunaDoTitulo = 1;
    private const int ColunaDoValor = 2;
    private const int QuantidadeDeColunas = 3;
    private const int NumerosDaParcela = 2;
    private const int PrimeiraParcela = 1;

    public static IReadOnlyList<CompraDaFatura> Ler(string? conteudo)
    {
        IReadOnlyList<string> linhas = SepararLinhas(conteudo);

        ExigirCabecalhoDoNubank(linhas);

        IReadOnlyList<CompraDaFatura> compras =
            [.. linhas.Skip(1).Select(MontarCompra).OfType<CompraDaFatura>()];

        return [.. JuntarParcelasAdiantadas(compras)
            .OrderByDescending(compra => compra.DataDaCompra)
            .ThenBy(compra => compra.Descricao, StringComparer.CurrentCultureIgnoreCase)];
    }

    public static Competencia? DeduzirMesDaFatura(string? nomeDoArquivo)
    {
        string nomeSemExtensao = Path.GetFileNameWithoutExtension(nomeDoArquivo ?? string.Empty);
        int posicaoDoSeparador = nomeSemExtensao.LastIndexOf(SeparadorDoNomeDoArquivo);

        if (posicaoDoSeparador < 0)
        {
            return null;
        }

        return TentarLerData(nomeSemExtensao[(posicaoDoSeparador + 1)..], out DateTime vencimento)
            ? Competencia.DaData(vencimento)
            : null;
    }

    private static IReadOnlyList<string> SepararLinhas(string? conteudo)
        => [.. (conteudo ?? string.Empty)
            .Split(FimDeLinha)
            .Select(linha => linha.Trim(RetornoDeCarro, MarcaDeOrdemDeBytes, ' '))
            .Where(linha => linha.Length > 0)];

    private static void ExigirCabecalhoDoNubank(IReadOnlyList<string> linhas)
    {
        bool comecaComOCabecalhoConhecido = linhas.Count > 0
            && string.Equals(
                linhas[0].Replace(" ", string.Empty),
                CabecalhoDoNubank,
                StringComparison.OrdinalIgnoreCase);

        if (!comecaComOCabecalhoConhecido)
        {
            throw new FormatException(MensagemDeArquivoInvalido);
        }
    }

    private static CompraDaFatura? MontarCompra(string linha)
    {
        IReadOnlyList<string> colunas = SepararColunas(linha);

        if (colunas.Count < QuantidadeDeColunas
            || !TentarLerData(colunas[ColunaDaData], out DateTime data)
            || !TentarLerValorGasto(colunas[ColunaDoValor], out decimal valor))
        {
            return null;
        }

        CompraDaFatura compra = new() { DataDaCompra = data, ValorDaParcela = valor };

        AplicarTitulo(compra, colunas[ColunaDoTitulo]);

        return compra;
    }

    private static void AplicarTitulo(CompraDaFatura compra, string titulo)
    {
        compra.Descricao = titulo.Trim();

        int posicaoDoMarcador = titulo.LastIndexOf(MarcadorDeParcela, StringComparison.CurrentCultureIgnoreCase);

        if (posicaoDoMarcador < 0)
        {
            return;
        }

        string[] numeros = titulo[(posicaoDoMarcador + MarcadorDeParcela.Length)..].Split(BarraDaParcela);

        if (numeros.Length != NumerosDaParcela
            || !TentarLerNumero(numeros[0], out int numeroDaParcela)
            || !TentarLerNumero(numeros[1], out int quantidadeParcelas)
            || numeroDaParcela < PrimeiraParcela
            || quantidadeParcelas < numeroDaParcela)
        {
            return;
        }

        compra.Descricao = titulo[..posicaoDoMarcador].Trim();
        compra.NumeroDaParcelaNaFatura = numeroDaParcela;
        compra.QuantidadeParcelas = quantidadeParcelas;
    }

    private static IReadOnlyList<string> SepararColunas(string linha)
    {
        List<string> colunas = [];
        StringBuilder colunaAtual = new();
        bool dentroDeAspas = false;

        foreach (char caractere in linha)
        {
            if (caractere == Aspas)
            {
                dentroDeAspas = !dentroDeAspas;
            }
            else if (caractere == SeparadorDeColunas && !dentroDeAspas)
            {
                colunas.Add(colunaAtual.ToString().Trim());
                colunaAtual.Clear();
            }
            else
            {
                colunaAtual.Append(caractere);
            }
        }

        colunas.Add(colunaAtual.ToString().Trim());

        return colunas;
    }

    private static bool TentarLerData(string texto, out DateTime data)
        => DateTime.TryParseExact(
            texto.Trim(), FormatoDaData, CultureInfo.InvariantCulture, DateTimeStyles.None, out data);

    private static bool TentarLerNumero(string texto, out int numero)
        => int.TryParse(texto.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out numero);

    private static bool TentarLerValorGasto(string texto, out decimal valor)
    {
        valor = 0m;

        return !texto.TrimStart().StartsWith(SinalDeNegativo)
            && Dinheiro.TentarConverter(texto, out valor)
            && valor > 0m;
    }

    private static IEnumerable<CompraDaFatura> JuntarParcelasAdiantadas(IReadOnlyList<CompraDaFatura> compras)
    {
        IEnumerable<CompraDaFatura> aVista = compras.Where(compra => !compra.EhParcelada);

        IEnumerable<CompraDaFatura> parceladas = compras
            .Where(compra => compra.EhParcelada)
            .GroupBy(compra => (compra.Descricao, compra.QuantidadeParcelas, compra.ValorDaParcela))
            .Select(mesmaCompra => mesmaCompra
                .OrderBy(compra => compra.NumeroDaParcelaNaFatura)
                .First());

        return aVista.Concat(parceladas);
    }
}
