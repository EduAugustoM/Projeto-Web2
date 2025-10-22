using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNet_MVC.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly FuncionariosServices services;
        private readonly CidadeServices cidadeServices; // ✅ Adicionado CidadeServices

        public FuncionariosController(FuncionariosServices _services, CidadeServices _cidadeServices) // ✅ Modificado construtor
        {
            services = _services;
            cidadeServices = _cidadeServices;
        }

        public IActionResult Index()
        {
            var listaFuncionarios = services.BuscarTodos();
            
            var todosEstados = cidadeServices.GetEstados();
            var todasCidades = cidadeServices.GetCidades();

            // Itera sobre os pacientes para preencher os nomes de exibição
            foreach (var funcionarios in listaFuncionarios)
            {
                if (int.TryParse(funcionarios.estado, out int estadoId))
                {
                    funcionarios.EstadoNome = todosEstados.FirstOrDefault(e => e.id == estadoId)?.name;
                }
                
                if (int.TryParse(funcionarios.cidade, out int cidadeId))
                {
                    funcionarios.CidadeNome = todasCidades.FirstOrDefault(c => c.id == cidadeId)?.name;
                }
            }

            return View("Listar", listaFuncionarios);
        }

        public IActionResult Cadastro(int codf = 0)
        {
            var model = services.BuscarFuncionario(codf);
            CarregarListas(model); // ✅ Carrega estados e cidades
            return View(model);
        }

        public IActionResult Excluir(int codf)
        {
            services.ExcluirFuncionario(codf);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Salvar(FuncionariosViewModel model)
        {
            if (!ModelState.IsValid) // ✅ Corrigida a validação
            {
                CarregarListas(model); // ✅ Recarrega listas se houver erro
                return View("Cadastro", model);
            }
            services.SalvarFuncionario(model);
            return RedirectToAction("Index");
        }

        // ✅ NOVO ENDPOINT: Recebe o ID do estado e retorna JSON com as cidades
        [HttpGet]
        public JsonResult GetCidadesPorEstado(int stateId)
        {
            try
            {
                // Busca todas as cidades da API
                var todasCidades = cidadeServices.GetCidades();
                
                // Filtra as cidades pelo state_id
                var cidadesDoEstado = todasCidades
                    .Where(c => c.state_id == stateId)
                    .Select(c => new SelectListItem 
                    { 
                        Value = c.id.ToString(),
                        Text = c.name 
                    })
                    .ToList();

                return Json(cidadesDoEstado);
            }
            catch (Exception ex)
            {
                return Json(new List<SelectListItem>());
            }
        }

        private void CarregarListas(FuncionariosViewModel model)
        {
            // Carregar estados - usando ID como valor
            model.Estados = cidadeServices.GetEstados()
                .Select(e => new SelectListItem { 
                    Value = e.id.ToString(),
                    Text = e.name 
                })
                .ToList();

            // Carregar cidades apenas se um estado já estiver selecionado
            if (!string.IsNullOrEmpty(model.estado) && int.TryParse(model.estado, out int estadoId))
            {
                var cidades = cidadeServices.GetCidades()
                    .Where(c => c.state_id == estadoId)
                    .Select(c => new SelectListItem { 
                        Value = c.id.ToString(),
                        Text = c.name 
                    })
                    .ToList();
                
                model.Cidades = cidades;
            }
            else
            {
                model.Cidades = new List<SelectListItem>();
            }
        }
    }
}