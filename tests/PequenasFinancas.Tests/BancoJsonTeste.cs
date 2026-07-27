using PequenasFinancas.Core.Comum;
using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Tests;

public sealed class BancoJsonTeste : IDisposable
{
    private readonly AmbienteDeTeste _ambiente = new();

    [Fact]
    public void DadosGravadosContinuamDisponiveisAoAbrirOAppDeNovo()
    {
        _ambiente.Cartoes.Salvar(new Cartao { Nome = "Nubank", Limite = 5000m });

        BancoJson bancoReaberto = new(_ambiente.Banco.CaminhoDoArquivo);
        bancoReaberto.Carregar();

        Assert.Equal("Nubank", bancoReaberto.Dados.Cartoes.Single().Nome);
    }

    [Fact]
    public void MesDeReferenciaEAjustesSaoGravadosEmTextoLegivel()
    {
        _ambiente.Rendas.Salvar(new FonteRenda
        {
            Descricao = "Salário",
            Valor = 6000m,
            VigenciaInicio = new Competencia(2026, 1),
            Ajustes = { [new Competencia(2026, 6)] = 6500m }
        });

        string conteudo = File.ReadAllText(_ambiente.Banco.CaminhoDoArquivo);

        Assert.Contains("\"vigenciaInicio\": \"2026-01\"", conteudo);
        Assert.Contains("\"2026-06\": 6500", conteudo);
        Assert.Contains("Salário", conteudo);
    }

    [Fact]
    public void CadaGravacaoGeraUmBackupDoArquivoAnterior()
    {
        _ambiente.Cartoes.Salvar(new Cartao { Nome = "Nubank" });
        _ambiente.Cartoes.Salvar(new Cartao { Nome = "Inter" });

        Assert.Single(Directory.GetFiles(_ambiente.Banco.PastaDeBackups, "dados-*.json"));
    }

    [Fact]
    public void BancoNovoComecaVazioSemQuebrar()
    {
        Assert.Empty(_ambiente.Cartoes.Listar());
        Assert.Empty(_ambiente.Rendas.Listar());
        Assert.Empty(_ambiente.Reservas.Listar());
    }

    public void Dispose() => _ambiente.Dispose();
}
