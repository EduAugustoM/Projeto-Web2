using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;

namespace AspNet_MVC.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly FuncionariosServices services;

        public FuncionariosController(FuncionariosServices _services)
        {
            services = _services;
        }

        public IActionResult Index()
        {
            var listaFuncionarios = services.BuscarTodos();
            return View("Listar", listaFuncionarios);
        }

        public IActionResult Cadastro(int codf = 0)
        {
            var model = services.BuscarFuncionario(codf);
            return View(model);
        }

        public IActionResult Excluir(int codf)
        {
            services.ExcluirFuncionario(codf);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Salvar(FuncionariosViewModel model) // Alterado para receber o ViewModel
        {
            if (ModelState.IsValid)
            {
                services.SalvarFuncionario(model);
            }
            return RedirectToAction("Index");
        }
    }
}