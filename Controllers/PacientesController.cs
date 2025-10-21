using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;

namespace AspNet_MVC.Controllers
{
    public class PacientesController : Controller
    {
        private readonly PacientesServices services;

        public PacientesController(PacientesServices _services)
        {
            services = _services;
        }

        public IActionResult Index()
        {
            var listaPacientes = services.BuscarTodos();
            return View("Listar", listaPacientes);
        }

        public IActionResult Cadastro(int codp = 0)
        {
            var model = services.BuscarPaciente(codp);
            return View(model);
        }

        public IActionResult Excluir(int codp)
        {
            services.ExcluirPaciente(codp);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Salvar(PacientesViewModel model)
        {
            if (ModelState.IsValid)
            {
                services.SalvarPaciente(model);
                return RedirectToAction("Index");
            }
            return View("Cadastro", model);
        }
    }
}