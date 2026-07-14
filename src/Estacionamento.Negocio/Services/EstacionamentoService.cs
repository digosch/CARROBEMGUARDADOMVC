using System.Globalization;
using Estacionamento.Dados.Interfaces;
using Estacionamento.Dominio.Entidades;
using Estacionamento.Dominio.Regras;
using Estacionamento.Negocio.Interfaces;

namespace Estacionamento.Negocio.Services;

public class EstacionamentoService : IEstacionamentoService
{
    private readonly IRegistroEstacionamentoRepository _registroDAO;
    private readonly ITabelaPrecoRepository _tabelaPrecoDAO;

    public EstacionamentoService(IRegistroEstacionamentoRepository registroDAO, ITabelaPrecoRepository tabelaPrecoDAO)
    {
        _registroDAO = registroDAO;
        _tabelaPrecoDAO = tabelaPrecoDAO;
    }

    public (bool Sucesso, string Mensagem) MarcarEntrada(string placa)
    {
        placa = placa.Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(placa))
        {
            return (false, "Informe a placa do veículo.");
        }

        var registroAberto = _registroDAO.BuscarAbertoPorPlaca(placa);
        if (registroAberto != null)
        {
            return (false, $"Já existe uma entrada em aberto para a placa {placa}.");
        }

        _registroDAO.Inserir(new RegistroEstacionamento
        {
            Placa = placa,
            DataHoraEntrada = DateTime.Now
        });

        return (true, $"Entrada registrada para a placa {placa}.");
    }

    public (bool Sucesso, string Mensagem) MarcarSaida(string placa)
    {
        placa = placa.Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(placa))
        {
            return (false, "Informe a placa do veículo.");
        }

        var registroAberto = _registroDAO.BuscarAbertoPorPlaca(placa);
        if (registroAberto == null)
        {
            return (false, $"Não foi encontrada uma entrada em aberto para a placa {placa}.");
        }

        var dataHoraSaida = DateTime.Now;
        var tabelaPreco = _tabelaPrecoDAO.BuscarVigentePorData(registroAberto.DataHoraEntrada);
        if (tabelaPreco == null)
        {
            return (false, "Não foi encontrada uma tabela de preços vigente para a data de entrada.");
        }

        var valor = CalculadoraValor.Calcular(
            registroAberto.DataHoraEntrada,
            dataHoraSaida,
            tabelaPreco.ValorHoraInicial,
            tabelaPreco.ValorHoraAdicional);

        _registroDAO.AtualizarSaida(registroAberto.Id, dataHoraSaida, tabelaPreco.Id, valor);

        return (true, $"Saída registrada para a placa {placa}. Valor cobrado: {valor.ToString("C", new CultureInfo("pt-BR"))}.");
    }

    public List<RegistroEstacionamento> ListarTodos()
    {
        return _registroDAO.ListarTodos();
    }
}
