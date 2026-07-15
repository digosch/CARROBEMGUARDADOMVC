using Microsoft.AspNetCore.Mvc;
using Estacionamento.Dominio.Regras;
using Estacionamento.Negocio.Interfaces;
using Estacionamento.Web.ViewModels;

namespace Estacionamento.Web.Controllers;

public class EstacionamentoController : Controller
{
    private readonly IEstacionamentoService _estacionamentoService;

    public EstacionamentoController(IEstacionamentoService estacionamentoService)
    {
        _estacionamentoService = estacionamentoService;
    }

    public IActionResult Index()
    {
        var registros = _estacionamentoService.ListarTodos();

        var listagem = registros.Select(r =>
        {
            TimeSpan? duracao = null;
            if (r.DataHoraSaida.HasValue)
            {
                var duracaoBruta = r.DataHoraSaida.Value - r.DataHoraEntrada;
                duracao = new TimeSpan(duracaoBruta.Days, duracaoBruta.Hours, duracaoBruta.Minutes, duracaoBruta.Seconds);
            }

            return new RegistroListagemViewModel
            {
                Placa = r.Placa,
                HorarioChegada = r.DataHoraEntrada,
                HorarioSaida = r.DataHoraSaida,
                Duracao = duracao,
                TempoCobradoHoras = duracao.HasValue
                    ? CalculadoraValor.CalcularHorasCobradas((int)duracao.Value.TotalMinutes)
                    : null,
                Preco = r.ValorHoraAplicado,
                ValorAPagar = r.ValorCobrado,
                EmAberto = r.EstaEmAberto
            };
        }).ToList();

        var viewModel = new EstacionamentoIndexViewModel
        {
            Registros = listagem
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult MarcarEntrada(MarcarEntradaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Sucesso"] = false;
            TempData["Mensagem"] = "Verifique os campos preenchidos.";
            return RedirectToAction("Index");
        }

        var (sucesso, mensagem) = _estacionamentoService.MarcarEntrada(model.Placa);
        TempData["Sucesso"] = sucesso;
        TempData["Mensagem"] = mensagem;
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult MarcarSaida(MarcarSaidaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Sucesso"] = false;
            TempData["Mensagem"] = "Verifique os campos preenchidos.";
            return RedirectToAction("Index");
        }

        var (sucesso, mensagem) = _estacionamentoService.MarcarSaida(model.Placa);
        TempData["Sucesso"] = sucesso;
        TempData["Mensagem"] = mensagem;
        return RedirectToAction("Index");
    }
}
