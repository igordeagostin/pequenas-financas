namespace PequenasFinancas.Core.Modelos;

public sealed class Categoria : IRegistro
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;
}
