using System.Runtime.InteropServices;
using AspNet_MVC.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Data;
using Models.Entidades;
using Models.Services;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class ConsultasController : Controller
{
    private readonly ConsultasServices services;
   
    public ConsultasController(ConsultasServices _services)
    {
        services = _services;       
    }

    public IActionResult Index()
    {       
        return View("Listar", services.BuscarTodos());
    }
    public IActionResult Cadastro(int codm = 0, DateTime? data = null, TimeSpan? hora = null)
    {
        var model = services.BuscaConsulta(codm, data, hora);
        return View(model);
    }

    public IActionResult Excluir(int codm, DateTime data, TimeSpan hora)
    {
        services.ExcluirConsulta(codm, data, hora);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(ConsultasViewModel model)
    {
        if (ModelState.IsValid)
        {
            services.SalvarConsulta(model);            
        }
        return RedirectToAction("Index");
    }
}