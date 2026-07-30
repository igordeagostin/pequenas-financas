using PequenasFinancas.App.Componentes.Compartilhados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.App.Servicos;

public sealed record TelaDoApp(string Nome, string Rota, NomeIcone Icone, TelaInicial? Abertura = null)
{
    public bool EhARaiz => Rota.Length == 0;
}
