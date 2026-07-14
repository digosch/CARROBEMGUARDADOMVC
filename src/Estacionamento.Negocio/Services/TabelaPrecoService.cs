using Estacionamento.Dados.Interfaces;
using Estacionamento.Dominio.Entidades;
using Estacionamento.Dominio.Regras;
using Estacionamento.Negocio.Interfaces;

namespace Estacionamento.Negocio.Services;

public class TabelaPrecoService : ITabelaPrecoService
{
    private readonly ITabelaPrecoRepository _tabelaPrecoDAO;

    public TabelaPrecoService(ITabelaPrecoRepository tabelaPrecoDAO)
    {
        _tabelaPrecoDAO = tabelaPrecoDAO;
    }

    public (bool Sucesso, string Mensagem) Cadastrar(DateTime dataInicioVigencia, DateTime dataFimVigencia, decimal valorHoraInicial, decimal valorHoraAdicional)
    {
        if (dataFimVigencia < dataInicioVigencia)
        {
            return (false, "A data fim da vigência não pode ser anterior à data início.");
        }

        var tabelasExistentes = _tabelaPrecoDAO.ListarTodos();
        var existeSobreposicao = VerificadorVigencia.ExisteSobreposicao(tabelasExistentes, dataInicioVigencia, dataFimVigencia);

        if (existeSobreposicao)
        {
            return (false, "Já existe uma tabela de preços cadastrada com vigência sobreposta a este período.");
        }

        _tabelaPrecoDAO.Inserir(new TabelaPreco
        {
            DataInicioVigencia = dataInicioVigencia,
            DataFimVigencia = dataFimVigencia,
            ValorHoraInicial = valorHoraInicial,
            ValorHoraAdicional = valorHoraAdicional
        });

        return (true, "Tabela de preços cadastrada com sucesso.");
    }

    public List<TabelaPreco> ListarTodos()
    {
        return _tabelaPrecoDAO.ListarTodos();
    }
}
