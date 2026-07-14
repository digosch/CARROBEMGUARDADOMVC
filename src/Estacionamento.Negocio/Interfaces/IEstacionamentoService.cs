using Estacionamento.Dominio.Entidades;

namespace Estacionamento.Negocio.Interfaces;

public interface IEstacionamentoService
{
    (bool Sucesso, string Mensagem) MarcarEntrada(string placa);
    (bool Sucesso, string Mensagem) MarcarSaida(string placa);
    List<RegistroEstacionamento> ListarTodos();
}
