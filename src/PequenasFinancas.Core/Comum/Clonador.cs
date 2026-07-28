using System.Text.Json;
using PequenasFinancas.Core.Dados;

namespace PequenasFinancas.Core.Comum;

public static class Clonador
{
    public static T Clonar<T>(T original)
        where T : class
        => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(original, OpcoesJson.Padrao), OpcoesJson.Padrao)
            ?? throw new InvalidOperationException($"Não foi possível copiar o registro do tipo {typeof(T).Name}.");
}
