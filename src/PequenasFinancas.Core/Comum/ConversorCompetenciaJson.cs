using System.Text.Json;
using System.Text.Json.Serialization;

namespace PequenasFinancas.Core.Comum;

public sealed class ConversorCompetenciaJson : JsonConverter<Competencia>
{
    public override Competencia Read(ref Utf8JsonReader leitor, Type tipo, JsonSerializerOptions opcoes)
        => Competencia.Analisar(leitor.GetString() ?? string.Empty);

    public override void Write(Utf8JsonWriter escritor, Competencia valor, JsonSerializerOptions opcoes)
        => escritor.WriteStringValue(valor.ToString());

    public override Competencia ReadAsPropertyName(ref Utf8JsonReader leitor, Type tipo, JsonSerializerOptions opcoes)
        => Competencia.Analisar(leitor.GetString() ?? string.Empty);

    public override void WriteAsPropertyName(Utf8JsonWriter escritor, Competencia valor, JsonSerializerOptions opcoes)
        => escritor.WritePropertyName(valor.ToString());
}
