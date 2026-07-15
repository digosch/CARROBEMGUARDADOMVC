namespace Estacionamento.Web.ViewModels;

public class EstacionamentoIndexViewModel
{
    public List<RegistroListagemViewModel> Registros { get; set; } = new();
    public MarcarEntradaViewModel Entrada { get; set; } = new();
    public MarcarSaidaViewModel Saida { get; set; } = new();
}
