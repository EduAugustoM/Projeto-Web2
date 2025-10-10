using System.Runtime.InteropServices;
using AspNet_MVC.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Data;
using Models.Entidades;
using Models.Services;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class AmbulatoriosController : Controller
{
    private readonly AmbulatoriosServices services;

    public AmbulatoriosController(AmbulatoriosServices _services)
    {
        services = _services;
    }

    public IActionResult Index()
    {
        return View("Listar", services.BuscarTodos());
    }

    public IActionResult Cadastro(int nroa = 0)
    {
        var model = services.BuscaAmbulatorio(nroa);
        return View(model);
    }
    public IActionResult Excluir(int nroa, int andar, int capacidade)
    {
        services.ExcluirAmbulatorio(nroa, andar, capacidade);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Salvar(AmbulatoriosViewModel model)
    {
        if (ModelState.IsValid)
        {
            services.SalvarAmbulatorio(model);
        }
        return RedirectToAction("Index");
    }
}