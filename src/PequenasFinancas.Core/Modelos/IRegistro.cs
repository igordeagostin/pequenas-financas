namespace PequenasFinancas.Core.Modelos;

/// <summary>Todo item guardado no banco tem identificador próprio.</summary>
public interface IRegistro
{
    Guid Id { get; set; }
}
