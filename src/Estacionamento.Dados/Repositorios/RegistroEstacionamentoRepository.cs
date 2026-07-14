using Microsoft.Data.SqlClient;
using Estacionamento.Dominio.Entidades;
using Estacionamento.Dados.Interfaces;

namespace Estacionamento.Dados.Repositorios;

public class RegistroEstacionamentoRepository : IRegistroEstacionamentoRepository
{
    private readonly string _connectionString;

    public RegistroEstacionamentoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public RegistroEstacionamento? BuscarAbertoPorPlaca(string placa)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(
            @"SELECT TOP 1 Id, Placa, DataHoraEntrada, DataHoraSaida, TabelaPrecoId, ValorCobrado
              FROM RegistrosEstacionamento
              WHERE Placa = @Placa AND DataHoraSaida IS NULL
              ORDER BY DataHoraEntrada DESC", conn);
        cmd.Parameters.AddWithValue("@Placa", placa);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return Mapear(reader);
        }
        return null;
    }

    public void Inserir(RegistroEstacionamento registro)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(
            @"INSERT INTO RegistrosEstacionamento (Placa, DataHoraEntrada)
              VALUES (@Placa, @DataHoraEntrada)", conn);
        cmd.Parameters.AddWithValue("@Placa", registro.Placa);
        cmd.Parameters.AddWithValue("@DataHoraEntrada", registro.DataHoraEntrada);

        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public void AtualizarSaida(int id, DateTime dataHoraSaida, int tabelaPrecoId, decimal valorCobrado)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(
            @"UPDATE RegistrosEstacionamento
              SET DataHoraSaida = @DataHoraSaida, TabelaPrecoId = @TabelaPrecoId, ValorCobrado = @ValorCobrado
              WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@DataHoraSaida", dataHoraSaida);
        cmd.Parameters.AddWithValue("@TabelaPrecoId", tabelaPrecoId);
        cmd.Parameters.AddWithValue("@ValorCobrado", valorCobrado);
        cmd.Parameters.AddWithValue("@Id", id);

        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public List<RegistroEstacionamento> ListarTodos()
    {
        var registros = new List<RegistroEstacionamento>();

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(
            @"SELECT r.Id, r.Placa, r.DataHoraEntrada, r.DataHoraSaida, r.ValorCobrado, tp.ValorHoraInicial
              FROM RegistrosEstacionamento r
              LEFT JOIN TabelaPrecos tp ON tp.Id = r.TabelaPrecoId
              ORDER BY r.DataHoraEntrada DESC", conn);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            registros.Add(MapearComPreco(reader));
        }

        return registros;
    }

    private static RegistroEstacionamento Mapear(SqlDataReader reader)
    {
        return new RegistroEstacionamento
        {
            Id = reader.GetInt32(0),
            Placa = reader.GetString(1),
            DataHoraEntrada = reader.GetDateTime(2),
            DataHoraSaida = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
            TabelaPrecoId = reader.IsDBNull(4) ? null : reader.GetInt32(4),
            ValorCobrado = reader.IsDBNull(5) ? null : reader.GetDecimal(5)
        };
    }

    private static RegistroEstacionamento MapearComPreco(SqlDataReader reader)
    {
        return new RegistroEstacionamento
        {
            Id = reader.GetInt32(0),
            Placa = reader.GetString(1),
            DataHoraEntrada = reader.GetDateTime(2),
            DataHoraSaida = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
            ValorCobrado = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
            ValorHoraAplicado = reader.IsDBNull(5) ? null : reader.GetDecimal(5)
        };
    }
}
