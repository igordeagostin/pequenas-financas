using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Modelos;
using PequenasFinancas.Core.Servicos;

namespace PequenasFinancas.Tests;

public sealed class ServicoRecorrenciaTeste
{
    [Fact]
    public void GastoFixoValeDoMesDeInicioEmDiante()
    {
        GastoFixo aluguel = new()
        {
            Descricao = "Aluguel",
            Valor = 1500.00m,
            VigenciaInicio = new Competencia(2026, 1)
        };

        Assert.False(ServicoRecorrencia.EstaVigenteEm(aluguel, new Competencia(2025, 12)));
        Assert.True(ServicoRecorrencia.EstaVigenteEm(aluguel, new Competencia(2026, 1)));
        Assert.True(ServicoRecorrencia.EstaVigenteEm(aluguel, new Competencia(2028, 5)));
    }

    [Fact]
    public void GastoFixoEncerradoDeixaDeValerDepoisDoMesFinal()
    {
        GastoFixo academia = new()
        {
            Descricao = "Academia",
            Valor = 120.00m,
            VigenciaInicio = new Competencia(2026, 1),
            VigenciaFim = new Competencia(2026, 6)
        };

        Assert.True(ServicoRecorrencia.EstaVigenteEm(academia, new Competencia(2026, 6)));
        Assert.False(ServicoRecorrencia.EstaVigenteEm(academia, new Competencia(2026, 7)));
    }

    [Fact]
    public void AjusteSubstituiOValorApenasNoMesInformado()
    {
        FonteRenda salario = new()
        {
            Descricao = "Salário",
            Valor = 6000.00m,
            VigenciaInicio = new Competencia(2026, 1),
            Ajustes = { [new Competencia(2026, 6)] = 6500.00m }
        };

        Assert.Equal(6000.00m, ServicoRecorrencia.ValorNoMes(salario, new Competencia(2026, 5)));
        Assert.Equal(6500.00m, ServicoRecorrencia.ValorNoMes(salario, new Competencia(2026, 6)));
        Assert.Equal(6000.00m, ServicoRecorrencia.ValorNoMes(salario, new Competencia(2026, 7)));
    }

    [Fact]
    public void SomaApenasOsLancamentosVigentesNoMes()
    {
        List<GastoFixo> gastos =
        [
            new() { Descricao = "Aluguel", Valor = 1500m, VigenciaInicio = new Competencia(2026, 1) },
            new() { Descricao = "Internet", Valor = 100m, VigenciaInicio = new Competencia(2026, 8) },
            new()
            {
                Descricao = "Academia",
                Valor = 120m,
                VigenciaInicio = new Competencia(2026, 1),
                VigenciaFim = new Competencia(2026, 6)
            }
        ];

        Assert.Equal(1500m, ServicoRecorrencia.SomarNoMes(gastos, new Competencia(2026, 7)));
        Assert.Equal(1620m, ServicoRecorrencia.SomarNoMes(gastos, new Competencia(2026, 5)));
        Assert.Equal(1600m, ServicoRecorrencia.SomarNoMes(gastos, new Competencia(2026, 8)));
    }
}
