using PequenasFinancas.Core.Dados;
using PequenasFinancas.Core.Modelos;

namespace PequenasFinancas.Core.Servicos;

public abstract class ServicoCrudCategorizado<T>(BancoJson banco, ServicoCategorias servicoCategorias)
    : ServicoCrud<T>(banco)
    where T : class, ICategorizavel
{
    protected override void AntesDeSalvar(T item)
        => item.Categoria = servicoCategorias.RegistrarNome(item.Categoria);
}
