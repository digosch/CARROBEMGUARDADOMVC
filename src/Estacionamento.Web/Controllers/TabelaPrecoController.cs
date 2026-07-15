using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Estacionamento.Negocio.Interfaces;
using Estacionamento.Web.ViewModels;

namespace Estacionamento.Web.Controllers;

public class TabelaPrecoController : Controller
{
    private readonly ITabelaPrecoService _tabelaPrecoService;

    public TabelaPrecoController(ITabelaPrecoService tabelaPrecoService)
    {
        _tabelaPrecoService = tabelaPrecoService;
    }

    public IActionResult Index()
    {
        var viewModel = new TabelaPrecoIndexViewModel
        {
            Tabelas = _tabelaPrecoService.ListarTodos()
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Cadastrar(TabelaPrecoIndexViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Sucesso"] = false;
            TempData["Mensagem"] = "Verifique os campos preenchidos (datas: dd/mm/aaaa, valores: 0.00).";
            return RedirectToAction("Index");
        }

        var formatoData = "dd/MM/yyyy";
        var culturaInvariante = CultureInfo.InvariantCulture;

        var dataInicioOk = DateTime.TryParseExact(model.NovaTabela.DataInicioVigencia, formatoData, culturaInvariante, DateTimeStyles.None, out var dataInicio);
        var dataFimOk = DateTime.TryParseExact(model.NovaTabela.DataFimVigencia, formatoData, culturaInvariante, DateTimeStyles.None, out var dataFim);
        var valorInicialOk = decimal.TryParse(model.NovaTabela.ValorHoraInicial, NumberStyles.Number, culturaInvariante, out var valorInicial);
        var valorAdicionalOk = decimal.TryParse(model.NovaTabela.ValorHoraAdicional, NumberStyles.Number, culturaInvariante, out var valorAdicional);

        if (!dataInicioOk || !dataFimOk || !valorInicialOk || !valorAdicionalOk)
        {
            TempData["Sucesso"] = false;
            TempData["Mensagem"] = "Não foi possível interpretar os valores informados.";
            return RedirectToAction("Index");
        }

        var (sucesso, mensagem) = _tabelaPrecoService.Cadastrar(dataInicio, dataFim, valorInicial, valorAdicional);
        TempData["Sucesso"] = sucesso;
        TempData["Mensagem"] = mensagem;
        return RedirectToAction("Index");
    }
}
