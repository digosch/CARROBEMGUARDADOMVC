using Estacionamento.Dominio.Entidades;

namespace Estacionamento.Dominio.Regras;

public static class VerificadorVigencia
{
    public static bool ExisteSobreposicao(IEnumerable<TabelaPreco> existentes, DateTime dataInicioVigencia, DateTime dataFimVigencia)
    {
        return existentes.Any(t =>
            dataInicioVigencia <= t.DataFimVigencia && dataFimVigencia >= t.DataInicioVigencia);
    }
}
