namespace Estacionamento.Dominio.Entidades;

public class RegistroEstacionamento
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public DateTime DataHoraEntrada { get; set; }
    public DateTime? DataHoraSaida { get; set; }
    public int? TabelaPrecoId { get; set; }
    public decimal? ValorCobrado { get; set; }
    public decimal? ValorHoraAplicado { get; set; }

    public bool EstaEmAberto => DataHoraSaida == null;
}
