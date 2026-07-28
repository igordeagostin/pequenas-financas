namespace PequenasFinancas.Core.Modelos;

public sealed class BancoDados
{
    public const int VersaoAtual = 1;

    public int Versao { get; set; } = VersaoAtual;

    public List<Cartao> Cartoes { get; set; } = [];

    public List<FonteRenda> Rendas { get; set; } = [];

    public List<RendaExtra> RendasExtras { get; set; } = [];

    public List<GastoFixo> GastosFixos { get; set; } = [];

    public List<CompraCartao> ComprasCartao { get; set; } = [];

    public List<Parcelamento> Parcelamentos { get; set; } = [];

    public List<Reserva> Reservas { get; set; } = [];

    public List<SemanaPlanejada> Semanas { get; set; } = [];
}
