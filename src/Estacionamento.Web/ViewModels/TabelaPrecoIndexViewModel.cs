using Estacionamento.Dominio.Entidades;

namespace Estacionamento.Web.ViewModels;

public class TabelaPrecoIndexViewModel
{
    public List<TabelaPreco> Tabelas { get; set; } = new();
    public CadastrarTabelaPrecoViewModel NovaTabela { get; set; } = new();
}
