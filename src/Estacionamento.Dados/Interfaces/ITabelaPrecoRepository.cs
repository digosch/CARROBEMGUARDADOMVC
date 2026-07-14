using Estacionamento.Dominio.Entidades;

namespace Estacionamento.Dados.Interfaces;

public interface ITabelaPrecoRepository
{
    TabelaPreco? BuscarVigentePorData(DateTime data);
    void Inserir(TabelaPreco tabelaPreco);
    List<TabelaPreco> ListarTodos();
}
