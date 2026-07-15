using Estacionamento.Dominio.Regras;
using Xunit;

namespace Estacionamento.Tests;

public class CalculadoraValorTests
{
    private static readonly DateTime Entrada = new(2024, 6, 10, 8, 0, 0);
    private const decimal ValorHoraInicial = 2.00m;
    private const decimal ValorHoraAdicional = 1.00m;

    [Theory]
    [InlineData(30, 1.00)]   // até 30 min: metade da hora inicial
    [InlineData(60, 2.00)]   // 1h00: hora inicial cheia
    [InlineData(70, 2.00)]   // 1h10: dentro da tolerância de 10 min
    [InlineData(75, 3.00)]   // 1h15: 1 hora adicional
    [InlineData(125, 3.00)]  // 2h05: ainda dentro da tolerância da 2ª hora
    [InlineData(135, 4.00)]  // 2h15: 2 horas adicionais
    public void Calcular_DeveRetornarValorEsperado(int minutosPermanencia, decimal valorEsperado)
    {
        var saida = Entrada.AddMinutes(minutosPermanencia);

        var valor = CalculadoraValor.Calcular(Entrada, saida, ValorHoraInicial, ValorHoraAdicional);

        Assert.Equal(valorEsperado, valor);
    }

    [Theory]
    [InlineData(10, 0.5)]
    [InlineData(70, 1)]
    [InlineData(135, 3)]
    public void CalcularHorasCobradas_DeveRetornarHorasEsperadas(int totalMinutos, decimal horasEsperadas)
    {
        var horas = CalculadoraValor.CalcularHorasCobradas(totalMinutos);

        Assert.Equal(horasEsperadas, horas);
    }
}
