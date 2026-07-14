namespace Estacionamento.Dominio.Regras;

public static class CalculadoraValor
{
    public static decimal Calcular(DateTime dataHoraEntrada, DateTime dataHoraSaida, decimal valorHoraInicial, decimal valorHoraAdicional)
    {
        var totalMinutos = (dataHoraSaida - dataHoraEntrada).TotalMinutes;

        if (totalMinutos <= 30)
        {
            return valorHoraInicial / 2;
        }

        var minutosAdicionais = totalMinutos - 60;
        if (minutosAdicionais <= 0)
        {
            return valorHoraInicial;
        }

        var excedente = minutosAdicionais - 10;
        if (excedente <= 0)
        {
            return valorHoraInicial;
        }

        var horasAdicionais = (int)Math.Ceiling(excedente / 60);
        return valorHoraInicial + horasAdicionais * valorHoraAdicional;
    }

    public static decimal CalcularHorasCobradas(int totalMinutos)
    {
        if (totalMinutos <= 30)
            return 0.5m;

        var minutosAdicionais = totalMinutos - 60;
        if (minutosAdicionais <= 0)
            return 1m;

        var excedente = minutosAdicionais - 10;
        if (excedente <= 0)
            return 1m;

        var horasAdicionais = Math.Ceiling(excedente / 60m);
        return 1m + horasAdicionais;
    }
}
