using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class PacientesController : Controller
{
    private readonly PacientesRepository repository;
    public PacientesController(PacientesRepository _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var ListaPacientes = repository.BuscarTodos();
        var NovaListaPacientes = ListaPacientes.Select(model => new PacientesViewModel
        {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca
        }).ToList();
        return View("Listar", NovaListaPacientes);
    }
    public IActionResult Cadastro(int codp = 0)
    {
        if (codp == 0)
        {
            PacientesViewModel model = new PacientesViewModel { codp = codp };
            return View(model);
        }
        else
        {
            var model = repository.Buscar(codp);
            PacientesViewModel newModel = new PacientesViewModel
            {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca
            };
            return View(newModel);
        }
    }

    public IActionResult Excluir(int codp)
    {
        repository.Excluir(new Pacientes{codp = codp});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(PacientesViewModel model)
    {
        if (ModelState.IsValid)
        {
            Pacientes newModel = new Pacientes
            {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca
            };
            if (model.codp == 0)
                repository.Salvar(newModel);
            else
                repository.Atualizar(newModel);
        }
        return RedirectToAction("Index");
    }
}