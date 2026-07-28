using System.Text.Json;
using PequenasFinancas.Core.Dados;

namespace PequenasFinancas.Core.Comum;

/// <summary>
/// Cria uma cópia independente de um registro. Usado ao abrir um formulário de edição,
/// para que cancelar não deixe alterações no dado guardado.
/// </summary>
public static class Clonador
{
    public static T Clonar<T>(T original)
        where T : class
        => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(original, OpcoesJson.Padrao), OpcoesJson.Padrao)
            ?? throw new InvalidOperationException($"Não foi possível copiar o registro do tipo {typeof(T).Name}.");
}
