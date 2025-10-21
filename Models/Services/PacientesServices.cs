using Models.Data;
using Models.Entidades;
using Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Models.Services
{
    public class PacientesServices
    {
        private readonly PacientesRepository repository;
        private readonly IWebHostEnvironment webHostEnvironment; 

        public PacientesServices(PacientesRepository _repository, IWebHostEnvironment _webHostEnvironment)
        {
            repository = _repository;
            webHostEnvironment = _webHostEnvironment;
        }

        public List<PacientesViewModel> BuscarTodos()
        {
            var listaPacientes = repository.BuscarTodos();
            var novaLista = listaPacientes.Select(model => new PacientesViewModel
            {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca,
                imagem = model.imagem
            }).ToList();
            return novaLista;
        }

        public PacientesViewModel BuscarPaciente(int codp = 0)
        {
            if (codp == 0)
            {
                return new PacientesViewModel { codp = codp };
            }
            else
            {
                var pacienteDb = repository.Buscar(codp);
                return new PacientesViewModel
                {
                    codp = pacienteDb.codp,
                    nome = pacienteDb.nome,
                    idade = pacienteDb.idade,
                    cidade = pacienteDb.cidade,
                    CPF = pacienteDb.CPF,
                    doenca = pacienteDb.doenca,
                    imagem = pacienteDb.imagem
                };
            }
        }

        public void SalvarPaciente(PacientesViewModel model)
        {
            string nomeImagemUnico = ProcessarImagem(model);

            Pacientes paciente = new Pacientes
            {
                codp = model.codp,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                CPF = model.CPF,
                doenca = model.doenca,
                imagem = nomeImagemUnico
            };

            if (model.codp == 0)
                repository.Salvar(paciente);
            else
                repository.Atualizar(paciente);
        }

        public void ExcluirPaciente(int codp)
        {
            var paciente = repository.Buscar(codp);
            
            if (paciente != null && !string.IsNullOrEmpty(paciente.imagem))
            {
                string caminhoImagem = Path.Combine(webHostEnvironment.WebRootPath, "imagens", "pacientes", paciente.imagem);
                if (File.Exists(caminhoImagem))
                {
                    File.Delete(caminhoImagem);
                }
            }

            repository.Excluir(new Pacientes { codp = codp });
        }

        private string ProcessarImagem(PacientesViewModel model)
        {
            string nomeImagem = null;
            string pastaImagens = Path.Combine(webHostEnvironment.WebRootPath, "imagens", "pacientes");

            if (model.ImagemArquivo != null)
            {
                if (!string.IsNullOrEmpty(model.imagem))
                {
                    string caminhoImagemAntiga = Path.Combine(pastaImagens, model.imagem);
                    if (File.Exists(caminhoImagemAntiga))
                    {
                        File.Delete(caminhoImagemAntiga);
                    }
                }

                nomeImagem = $"{Guid.NewGuid()}{Path.GetExtension(model.ImagemArquivo.FileName)}";
                string caminhoCompleto = Path.Combine(pastaImagens, nomeImagem);
                using (var fileStream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    model.ImagemArquivo.CopyTo(fileStream);
                }
            }
            else if (model.codp != 0)
            {
                var pacienteExistente = repository.Buscar(model.codp);
                nomeImagem = pacienteExistente.imagem;
            }
            
            return nomeImagem;
        }
    }
}