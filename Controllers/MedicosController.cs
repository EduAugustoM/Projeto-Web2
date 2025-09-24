using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class MedicosController : Controller
{
    private readonly MedicosRepository repository;
    public MedicosController(MedicosRepository _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var ListaMedicos = repository.BuscarTodos();
        var NovaListaMedicos = ListaMedicos.Select(model => new MedicosViewModel
        {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                CPF = model.CPF,
                cidade = model.cidade,
                nroa = model.nroa
        }).ToList();
        return View("Listar", NovaListaMedicos);
    }
    public IActionResult Cadastro(int codm = 0)
    {
        if (codm == 0)
        {
            MedicosViewModel model = new MedicosViewModel { codm = codm };
            model.Ambulatorios = repository.BuscarTodos().Select(
                a => new SelectListItem
                {
                    Value = a.nroa.ToString(),
                    Text = a.nroa
                }
            ).ToList();
            return View(model);
        }
        else
        {
            var model = repository.Buscar(codm);
            MedicosViewModel newModel = new MedicosViewModel
            {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                CPF = model.CPF,
                cidade = model.cidade,
                nroa = model.nroa
            };
            newModel.Ambulatorios = repository.BuscarTodos().Select(
                a => new SelectListItem
                {
                    Value = a.nroa.ToString(),
                    Text = a.nroa
                }
            ).ToList();
            return View(newModel);
        }
    }

    public IActionResult Excluir(int codm)
    {
        repository.Excluir(new Medicos{codm = codm});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(MedicosViewModel model)
    {
        if (ModelState.IsValid)
        {
            Medicos medicos = new Medicos
            {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                CPF = model.CPF,
                cidade = model.cidade,
                nroa = model.nroa
            };
            if (model.codm == 0)
            {
                repository.Salvar(medicos);
            }
            else
            {
                repository.Atualizar(medicos);
            }
            return RedirectToAction("Index");
        }
        else
        {
            return View("Cadastro", model);
        }
    }
}