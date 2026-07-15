using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Web.ViewModels;

public class CadastrarTabelaPrecoViewModel
{
    [Required(ErrorMessage = "Informe a data de início")]
    [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Use o formato dd/mm/aaaa")]
    public string DataInicioVigencia { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a data de fim")]
    [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Use o formato dd/mm/aaaa")]
    public string DataFimVigencia { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o valor da hora inicial")]
    [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Use o formato 0.00")]
    public string ValorHoraInicial { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o valor da hora adicional")]
    [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Use o formato 0.00")]
    public string ValorHoraAdicional { get; set; } = string.Empty;
}
