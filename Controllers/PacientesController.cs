using Microsoft.AspNetCore.Mvc;
using Models.Services;
using Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Data;
using Models.Entidades;

namespace AspNet_MVC.Controllers
{
    public class PacientesController : Controller
    {
        private readonly PacientesServices services;
        private readonly CidadeServices cidadeServices;

        public PacientesController(PacientesServices _services, CidadeServices _cidadeServices)
        {
            services = _services;
            cidadeServices = _cidadeServices;
        }

        public IActionResult Index()
        {
            var listaPacientes = services.BuscarTodos(); // Retorna List<PacientesViewModel>
            
            // Carrega as listas completas de estados e cidades UMA VEZ
            // (Melhoria: Considere cachear isso se as listas forem grandes/estáticas)
            var todosEstados = cidadeServices.GetEstados();
            var todasCidades = cidadeServices.GetCidades();

            // Itera sobre os pacientes para preencher os nomes de exibição
            foreach (var paciente in listaPacientes)
            {
                if (int.TryParse(paciente.estado, out int estadoId))
                {
                    paciente.EstadoNome = todosEstados.FirstOrDefault(e => e.id == estadoId)?.name;
                }
                
                if (int.TryParse(paciente.cidade, out int cidadeId))
                {
                    // Busca o nome da cidade correspondente ao ID
                    paciente.CidadeNome = todasCidades.FirstOrDefault(c => c.id == cidadeId)?.name;
                }
            }

            return View("Listar", listaPacientes);
        }

        public IActionResult Cadastro(int codp = 0)
        {
            var model = services.BuscarPaciente(codp);
            CarregarListas(model);
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
            if (!ModelState.IsValid)
            {
                CarregarListas(model);
                return View("Cadastro", model);
            }
            services.SalvarPaciente(model);
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
                        Value = c.id.ToString(), // Usando ID como valor
                        Text = c.name 
                    })
                    .ToList();

                return Json(cidadesDoEstado);
            }
            catch (Exception ex)
            {
                // Log do erro (implemente conforme sua necessidade)
                return Json(new List<SelectListItem>());
            }
        }

        // ✅ ENDPOINT ALTERNATIVO: Caso prefira buscar por nome do estado
        [HttpGet]
        public JsonResult GetCidadesPorNomeEstado(string estadoNome)
        {
            try
            {
                // Busca todos os estados para encontrar o ID
                var estados = cidadeServices.GetEstados();
                var estado = estados.FirstOrDefault(e => 
                    e.name.Equals(estadoNome, StringComparison.OrdinalIgnoreCase) || 
                    e.abbr.Equals(estadoNome, StringComparison.OrdinalIgnoreCase));

                if (estado != null)
                {
                    // Busca cidades pelo state_id
                    var todasCidades = cidadeServices.GetCidades();
                    var cidadesDoEstado = todasCidades
                        .Where(c => c.state_id == estado.id)
                        .Select(c => new SelectListItem 
                        { 
                            Value = c.id.ToString(), 
                            Text = c.name 
                        })
                        .ToList();

                    return Json(cidadesDoEstado);
                }

                return Json(new List<SelectListItem>());
            }
            catch (Exception ex)
            {
                return Json(new List<SelectListItem>());
            }
        }

        private void CarregarListas(PacientesViewModel model)
        {
            // Carregar estados - usando ID como valor
            model.Estados = cidadeServices.GetEstados()
                .Select(e => new SelectListItem { 
                    Value = e.id.ToString(), // ✅ Usando ID como valor
                    Text = e.name 
                })
                .ToList();

            // Carregar cidades apenas se um estado já estiver selecionado
            if (!string.IsNullOrEmpty(model.estado) && int.TryParse(model.estado, out int estadoId))
            {
                var cidades = cidadeServices.GetCidades()
                    .Where(c => c.state_id == estadoId)
                    .Select(c => new SelectListItem { 
                        Value = c.id.ToString(), // ✅ Usando ID como valor
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