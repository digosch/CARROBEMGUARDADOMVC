namespace Estacionamento.Web.ViewModels;

public class RegistroListagemViewModel
{
    public string Placa { get; set; } = string.Empty;
    public DateTime HorarioChegada { get; set; }
    public DateTime? HorarioSaida { get; set; }
    public TimeSpan? Duracao { get; set; }
    public decimal? TempoCobradoHoras { get; set; }
    public decimal? Preco { get; set; }
    public decimal? ValorAPagar { get; set; }
    public bool EmAberto { get; set; }
}
