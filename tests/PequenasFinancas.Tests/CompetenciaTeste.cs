using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Tests;

public sealed class CompetenciaTeste
{
    [Fact]
    public void AvancarMesesViraOAnoCorretamente()
    {
        Competencia dezembro = new(2026, 12);

        Assert.Equal(new Competencia(2027, 1), dezembro.Adicionar(1));
        Assert.Equal(new Competencia(2027, 12), dezembro.Adicionar(12));
    }

    [Fact]
    public void VoltarMesesViraOAnoCorretamente()
    {
        Competencia janeiro = new(2026, 1);

        Assert.Equal(new Competencia(2025, 12), janeiro.Anterior());
        Assert.Equal(new Competencia(2025, 1), janeiro.Adicionar(-12));
    }

    [Fact]
    public void CalculaADistanciaEmMesesEntreDuasCompetencias()
    {
        Assert.Equal(11, new Competencia(2027, 3).DiferencaEmMesesDe(new Competencia(2026, 4)));
        Assert.Equal(-2, new Competencia(2026, 2).DiferencaEmMesesDe(new Competencia(2026, 4)));
        Assert.Equal(0, new Competencia(2026, 7).DiferencaEmMesesDe(new Competencia(2026, 7)));
    }

    [Fact]
    public void VigenciaSemFimValeParaQualquerMesFuturo()
    {
        Competencia inicio = new(2026, 1);

        Assert.True(new Competencia(2030, 8).EstaEntre(inicio, null));
        Assert.False(new Competencia(2025, 12).EstaEntre(inicio, null));
    }

    [Fact]
    public void VigenciaComFimRespeitaOsDoisLimites()
    {
        Competencia inicio = new(2026, 3);
        Competencia fim = new(2026, 6);

        Assert.True(new Competencia(2026, 3).EstaEntre(inicio, fim));
        Assert.True(new Competencia(2026, 6).EstaEntre(inicio, fim));
        Assert.False(new Competencia(2026, 7).EstaEntre(inicio, fim));
    }

    [Fact]
    public void GravaELeNoFormatoDoArquivo()
    {
        Competencia competencia = new(2026, 7);

        Assert.Equal("2026-07", competencia.ToString());
        Assert.Equal(competencia, Competencia.Analisar("2026-07"));
    }

    [Theory]
    [InlineData("2026")]
    [InlineData("2026-13")]
    [InlineData("julho/2026")]
    [InlineData("")]
    public void TextoInvalidoNaoViraCompetencia(string texto)
    {
        Assert.False(Competencia.TentarAnalisar(texto, out _));
    }

    [Fact]
    public void MostraOMesPorExtensoEmPortugues()
    {
        Assert.Equal("Julho / 2026", new Competencia(2026, 7).NomeExtenso);
        Assert.Equal("07/2026", new Competencia(2026, 7).NomeCurto);
    }

    [Fact]
    public void ComparaCompetenciasNaOrdemDoCalendario()
    {
        Assert.True(new Competencia(2026, 1) < new Competencia(2026, 2));
        Assert.True(new Competencia(2027, 1) > new Competencia(2026, 12));
    }

    [Fact]
    public void OrdenaCompetenciasMesmoSemSaberOTipo()
    {
        IComparable janeiro = new Competencia(2026, 1);

        Assert.True(janeiro.CompareTo(new Competencia(2026, 2)) < 0);
        Assert.Equal(0, janeiro.CompareTo(new Competencia(2026, 1)));
    }

    [Fact]
    public void CompararComOutroTipoNaoEhPermitido()
        => Assert.Throws<ArgumentException>(() => ((IComparable)new Competencia(2026, 1)).CompareTo("2026-01"));
}
