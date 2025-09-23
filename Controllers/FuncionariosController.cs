using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;
namespace AspNet_MVC.Controllers;

public class FuncionariosController : Controller
{
    private readonly FuncionariosRepository repository;
    public FuncionariosController(FuncionariosRepository _repository)
    {
        repository = _repository;
    }

    public IActionResult Index()
    {
        var ListaFuncionarios = repository.BuscarTodos();
        var NovaListaFuncionarios = ListaFuncionarios.Select(model => new FuncionariosViewModel
        {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                salario = model.salario,
                CPF = model.CPF
        }).ToList();
        return View("Listar", NovaListaFuncionarios);
    }
    public IActionResult Cadastro(int codf = 0)
    {
        if (codf == 0)
        {
            Funcionarios model = new Funcionarios { codf = codf };
            return View(model);
        }
        else
        {
            var model = repository.Buscar(codf);
            Funcionarios newModel = new Funcionarios
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                salario = model.salario,
                CPF = model.CPF
            };
            return View(model);
        }
    }

    public IActionResult Excluir(int codf)
    {
        repository.Excluir(new Funcionarios { codf = codf });
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(Funcionarios model)
    {
        if (ModelState.IsValid)
        {
            Funcionarios novoFuncionario = new Funcionarios
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                salario = model.salario,
                CPF = model.CPF
            };
            if (model.codf == 0)
            {
                repository.Salvar(novoFuncionario);
            }
            else
            {
                repository.Atualizar(novoFuncionario);
            }
            return RedirectToAction("Index");
        }
        else
        {
            return View("Cadastro", model);
        }
    }
}