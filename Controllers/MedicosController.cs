using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;

namespace AspNet_MVC.Controllers
{
    public class MedicosController : Controller
    {
        private readonly MedicosServices services;

        public MedicosController(MedicosServices _services)
        {
            services = _services;
        }

        public IActionResult Index()
        {
            var listaMedicos = services.BuscarTodos();
            return View("Listar", listaMedicos);
        }

        public IActionResult Cadastro(int codm = 0)
        {
            var model = services.BuscarMedico(codm);
            return View(model);
        }

        public IActionResult Excluir(int codm)
        {
            services.ExcluirMedico(codm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Salvar(MedicosViewModel model)
        {
            if (ModelState.IsValid)
            {
                services.SalvarMedico(model);
                return RedirectToAction("Index");
            }
            else
            {
                var modelComAmbulatorios = services.BuscarMedico(model.codm);
                model.Ambulatorios = modelComAmbulatorios.Ambulatorios;
                return View("Cadastro", model);
            }
        }
    }
}