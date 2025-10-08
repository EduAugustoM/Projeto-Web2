using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;

namespace AspNet_MVC.Controllers;

public class PacientesController : Controller
{
    private readonly PacientesRepository repository;
    private readonly IWebHostEnvironment webHostEnvironment;

    public PacientesController(PacientesRepository _repository, IWebHostEnvironment _webHostEnvironment)
    {
        repository = _repository;
        webHostEnvironment = _webHostEnvironment;
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
            doenca = model.doenca,
            imagem = model.imagem
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
                doenca = model.doenca,
                imagem = model.imagem
            };
            return View(newModel);
        }
    }

    public IActionResult Excluir(int codp)
    {
        var paciente = repository.Buscar(codp);
        
        // Remove a imagem do servidor se existir
        if (!string.IsNullOrEmpty(paciente.imagem))
        {
            string caminhoImagem = Path.Combine(webHostEnvironment.WebRootPath, "imagens", "pacientes", paciente.imagem);
            if (System.IO.File.Exists(caminhoImagem))
            {
                System.IO.File.Delete(caminhoImagem);
            }
        }

        repository.Excluir(new Pacientes { codp = codp });
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Salvar(PacientesViewModel model)
    {
        if (ModelState.IsValid)
        {
            string nomeImagem = string.Empty;

            // Processa o upload da imagem se houver
            if (model.ImagemArquivo != null && model.ImagemArquivo.Length > 0)
            {
                // Cria o diretório se não existir
                string pastaImagens = Path.Combine(webHostEnvironment.WebRootPath, "imagens", "pacientes");
                if (!Directory.Exists(pastaImagens))
                {
                    Directory.CreateDirectory(pastaImagens);
                }

                // Gera um nome único para a imagem
                string extensao = Path.GetExtension(model.ImagemArquivo.FileName);
                nomeImagem = $"{Guid.NewGuid()}{extensao}";
                string caminhoCompleto = Path.Combine(pastaImagens, nomeImagem);

                // Salva o arquivo
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    model.ImagemArquivo.CopyTo(stream);
                }

                // Se for uma atualização e já existia uma imagem, remove a antiga
                if (model.codp != 0 && !string.IsNullOrEmpty(model.imagem))
                {
                    string imagemAntiga = Path.Combine(pastaImagens, model.imagem);
                    if (System.IO.File.Exists(imagemAntiga))
                    {
                        System.IO.File.Delete(imagemAntiga);
                    }
                }
            }
            else if (model.codp != 0)
            {
                // Se está atualizando mas não enviou nova imagem, mantém a anterior
                var pacienteExistente = repository.Buscar(model.codp);
                nomeImagem = pacienteExistente.imagem;
            }

            Pacientes newModel = new Pacientes
            {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca,
                imagem = nomeImagem
            };

            if (model.codp == 0)
                repository.Salvar(newModel);
            else
                repository.Atualizar(newModel);
        }
        return RedirectToAction("Index");
    }
}