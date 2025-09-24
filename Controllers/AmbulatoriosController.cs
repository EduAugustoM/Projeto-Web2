using System.Runtime.InteropServices;
using AspNet_MVC.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class AmbulatoriosController : Controller
{
    private readonly AmbulatoriosRepository repository;
    private readonly MedicosRepository repositoryMedicos;

    public AmbulatoriosController(AmbulatoriosRepository _repository, MedicosRepository _repositoryMedicos)
    {
        repository = _repository;
        repositoryMedicos = _repositoryMedicos;
    }

    public IActionResult Index()
    {
        var ListaAmbulatorios = repository.BuscarTodos();
        var NovaListaAmbulatorios = ListaAmbulatorios.Select(model => new AmbulatoriosViewModel
        {
            nroa = model.nroa,
            andar = model.andar,
            capacidade = model.capacidade
        }).ToList();
        return View("Listar", NovaListaAmbulatorios);
    }
    public IActionResult Cadastro(int nroa = 0)
    {
        if (nroa == 0)
        {
            AmbulatoriosViewModel model = new AmbulatoriosViewModel { nroa = nroa };
            return View(model);
        }
        else
        {
            var model = repository.Buscar(nroa);
            AmbulatoriosViewModel newModel = new AmbulatoriosViewModel
            {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
            };
            return View(newModel);
        }
    }
    public IActionResult Excluir(int nroa)
    {
        repository.Excluir(new Ambulatorios { nroa = nroa });
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Salvar(AmbulatoriosViewModel model)
    {
        if (ModelState.IsValid)
        {
            Ambulatorios ambulatorio = new Ambulatorios
            {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
            };
            repository.Salvar(ambulatorio);
        }
        return RedirectToAction("Index");
    }
}