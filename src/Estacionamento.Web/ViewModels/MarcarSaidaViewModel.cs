using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Web.ViewModels;

public class MarcarSaidaViewModel
{
    [Required(ErrorMessage = "Informe a placa")]
    [StringLength(8, MinimumLength = 7, ErrorMessage = "Placa inválida")]
    public string Placa { get; set; } = string.Empty;
}
