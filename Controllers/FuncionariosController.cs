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
        return View("Listar", ListaFuncionarios);
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
            return View(model);
        }
    }

    public IActionResult Excluir(int codf)
    {
        repository.Excluir(new Funcionarios{codf = codf});
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(Funcionarios model)
    {
        if (!ModelState.IsValid)
        {
            return View("Cadastro", model);
        }
        Funcionarios funcionarios = new Funcionarios
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
                repository.Salvar(funcionarios);