using Estacionamento.Dominio.Entidades;

namespace Estacionamento.Negocio.Interfaces;

public interface ITabelaPrecoService
{
    (bool Sucesso, string Mensagem) Cadastrar(DateTime dataInicioVigencia, DateTime dataFimVigencia, decimal valorHoraInicial, decimal valorHoraAdicional);
    List<TabelaPreco> ListarTodos();
}
