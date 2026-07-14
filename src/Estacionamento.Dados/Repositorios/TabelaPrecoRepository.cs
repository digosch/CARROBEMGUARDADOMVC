using Microsoft.Data.SqlClient;
using Estacionamento.Dominio.Entidades;
using Estacionamento.Dados.Interfaces;

namespace Estacionamento.Dados.Repositorios;

public class TabelaPrecoRepository : ITabelaPrecoRepository
{
    private readonly string _connectionString;

    public TabelaPrecoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public TabelaPreco? BuscarVigentePorData(DateTime data)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(
            @"SELECT TOP 1 Id, DataInicioVigencia, DataFimVigencia, ValorHoraInicial, ValorHoraAdicional, DataCriacao
              FROM TabelaPrecos
              WHERE @Data >= DataInicioVigencia AND @Data <= DataFimVigencia
              ORDER BY DataInicioVigencia DESC", conn);
        cmd.Parameters.AddWithValue("@Data", data.Date);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return Mapear(reader);
        }
        return null;
    }

    public void Inserir(TabelaPreco tabelaPreco)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(
            @"INSERT INTO TabelaPrecos (DataInicioVigencia, DataFimVigencia, ValorHoraInicial, ValorHoraAdicional)
              VALUES (@DataInicioVigencia, @DataFimVigencia, @ValorHoraInicial, @ValorHoraAdicional)", conn);
        cmd.Parameters.AddWithValue("@DataInicioVigencia", tabelaPreco.DataInicioVigencia.Date);
        cmd.Parameters.AddWithValue("@DataFimVigencia", tabelaPreco.DataFimVigencia.Date);
        cmd.Parameters.AddWithValue("@ValorHoraInicial", tabelaPreco.ValorHoraInicial);
        cmd.Parameters.AddWithValue("@ValorHoraAdicional", tabelaPreco.ValorHoraAdicional);

        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public List<TabelaPreco> ListarTodos()
    {
        var tabelas = new List<TabelaPreco>();

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(
            @"SELECT Id, DataInicioVigencia, DataFimVigencia, ValorHoraInicial, ValorHoraAdicional, DataCriacao
              FROM TabelaPrecos
              ORDER BY DataInicioVigencia DESC", conn);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            tabelas.Add(Mapear(reader));
        }

        return tabelas;
    }

    private static TabelaPreco Mapear(SqlDataReader reader)
    {
        return new TabelaPreco
        {
            Id = reader.GetInt32(0),
            DataInicioVigencia = reader.GetDateTime(1),
            DataFimVigencia = reader.GetDateTime(2),
            ValorHoraInicial = reader.GetDecimal(3),
            ValorHoraAdicional = reader.GetDecimal(4),
            DataCriacao = reader.GetDateTime(5)
        };
    }
}
