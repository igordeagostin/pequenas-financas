using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using PequenasFinancas.Core.Comum;

namespace PequenasFinancas.Core.Dados;

/// <summary>Configuração única de leitura e gravação do JSON, usada pelo banco e pelo clonador.</summary>
public static class OpcoesJson
{
    public static JsonSerializerOptions Padrao { get; } = Criar();

    private static JsonSerializerOptions Criar()
    {
        JsonSerializerOptions opcoes = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        opcoes.Converters.Add(new ConversorCompetenciaJson());
        opcoes.Converters.Add(new JsonStringEnumConverter());

        return opcoes;
    }
}
