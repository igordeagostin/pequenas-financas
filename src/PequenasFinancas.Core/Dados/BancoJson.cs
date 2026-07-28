using System.Text.Json;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Dados;

/// <summary>
/// Guarda todos os dados do app em um único arquivo JSON.
/// A gravação é atômica e mantém um histórico curto de backups.
/// </summary>
public sealed class BancoJson
{
    private const string NomeDoArquivo = "dados.json";
    private const string NomeDaPastaDeBackups = "backups";
    private const string ExtensaoTemporaria = ".tmp";
    private const int QuantidadeDeBackupsMantidos = 10;

    private readonly Lock _travaDeGravacao = new();
    private BancoDados _dados = new();
    private bool _carregado;

    public BancoJson()
        : this(CaminhoPadrao())
    {
    }

    public BancoJson(string caminhoDoArquivo)
    {
        CaminhoDoArquivo = caminhoDoArquivo;
    }

    /// <summary>Disparado após qualquer gravação, para a interface se atualizar.</summary>
    public event Action? DadosAlterados;

    public string CaminhoDoArquivo { get; }

    public string PastaDeBackups
        => Path.Combine(Path.GetDirectoryName(CaminhoDoArquivo) ?? ".", NomeDaPastaDeBackups);

    public BancoDados Dados
    {
        get
        {
            if (!_carregado)
            {
                Carregar();
            }

            return _dados;
        }
    }

    public static string CaminhoPadrao()
        => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PequenasFinancas",
            NomeDoArquivo);

    public void Carregar()
    {
        lock (_travaDeGravacao)
        {
            GarantirPasta();

            _dados = File.Exists(CaminhoDoArquivo)
                ? LerArquivo()
                : new BancoDados();

            _carregado = true;
        }
    }

    /// <summary>Grava o arquivo e avisa a interface. Chamado por todo serviço após alterar dados.</summary>
    public void Salvar()
    {
        lock (_travaDeGravacao)
        {
            GarantirPasta();
            GerarBackup();

            string caminhoTemporario = CaminhoDoArquivo + ExtensaoTemporaria;
            File.WriteAllText(caminhoTemporario, JsonSerializer.Serialize(_dados, OpcoesJson.Padrao));
            File.Move(caminhoTemporario, CaminhoDoArquivo, overwrite: true);
        }

        DadosAlterados?.Invoke();
    }

    private BancoDados LerArquivo()
    {
        string conteudo = File.ReadAllText(CaminhoDoArquivo);

        if (string.IsNullOrWhiteSpace(conteudo))
        {
            return new BancoDados();
        }

        return JsonSerializer.Deserialize<BancoDados>(conteudo, OpcoesJson.Padrao) ?? new BancoDados();
    }

    private void GarantirPasta()
    {
        string pasta = Path.GetDirectoryName(CaminhoDoArquivo) ?? ".";
        Directory.CreateDirectory(pasta);
    }

    private void GerarBackup()
    {
        if (!File.Exists(CaminhoDoArquivo))
        {
            return;
        }

        Directory.CreateDirectory(PastaDeBackups);

        string nomeDoBackup = $"dados-{DateTime.Now:yyyyMMdd-HHmmss-fff}.json";
        File.Copy(CaminhoDoArquivo, Path.Combine(PastaDeBackups, nomeDoBackup), overwrite: true);

        RemoverBackupsAntigos();
    }

    private void RemoverBackupsAntigos()
    {
        IEnumerable<FileInfo> backupsExcedentes = new DirectoryInfo(PastaDeBackups)
            .GetFiles("dados-*.json")
            .OrderByDescending(arquivo => arquivo.Name)
            .Skip(QuantidadeDeBackupsMantidos);

        foreach (FileInfo backup in backupsExcedentes)
        {
            backup.Delete();
        }
    }
}
