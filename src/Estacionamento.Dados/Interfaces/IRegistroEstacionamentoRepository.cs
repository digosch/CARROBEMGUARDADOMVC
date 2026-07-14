using Estacionamento.Dominio.Entidades;

namespace Estacionamento.Dados.Interfaces;

public interface IRegistroEstacionamentoRepository
{
    RegistroEstacionamento? BuscarAbertoPorPlaca(string placa);
    void Inserir(RegistroEstacionamento registro);
    void AtualizarSaida(int id, DateTime dataHoraSaida, int tabelaPrecoId, decimal valorCobrado);
    List<RegistroEstacionamento> ListarTodos();
}
