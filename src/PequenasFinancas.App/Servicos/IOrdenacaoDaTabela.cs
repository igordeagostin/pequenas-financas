namespace PequenasFinancas.App.Servicos;

public interface IOrdenacaoDaTabela
{
    bool Crescente { get; }

    bool EstaOrdenandoPor(string nome);

    void Alternar(string nome);
}
