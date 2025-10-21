using Models.ViewModel;
using AspNet_MVC.Models.Entidades;
using Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Services
{
    public class ConsultasServices
    {
        private readonly ConsultasRepository repository;
        private readonly PacientesRepository repositoryPacientes;
        private readonly MedicosRepository repositoryMedicos;

        public ConsultasServices(ConsultasRepository _repository, PacientesRepository _repositoryPacientes, MedicosRepository _repositoryMedicos)
        {
            this.repository = _repository;
            this.repositoryMedicos = _repositoryMedicos;
            this.repositoryPacientes = _repositoryPacientes;
        }

        public void SalvarConsulta(ConsultasViewModel model)
        {
            Consultas newModel = new Consultas
            {
                codm = model.codm,
                codp = model.codp,
                data = model.data,
                hora = model.hora
            };


            repository.Salvar(newModel);
        }

        public ConsultasViewModel BuscaConsulta()
        {
            var model = new ConsultasViewModel();
            model.data = DateTime.Today;
            model.Pacientes = repositoryPacientes.BuscarTodos().Select(
                item => new SelectListItem
                {
                    Value = item.codp.ToString(),
                    Text = item.nome
                }
            ).ToList();

            model.Medicos = repositoryMedicos.BuscarTodos().Select(
                item => new SelectListItem
                {
                    Value = item.codm.ToString(),
                    Text = item.nome
                }
            ).ToList();
            
            return model;
        }

        public List<ConsultasViewModel> BuscarTodos()
        {
            var ListaPacientes = repository.BuscarTodos();
            var NovaListaPacientes = ListaPacientes.Select(model => new ConsultasViewModel
            {
                codm = model.codm,
                codp = model.codp,
                data = model.data,
                hora = model.hora,
            }).ToList();

            return NovaListaPacientes;
        }
        public void ExcluirConsulta(int codm, DateTime data, TimeSpan hora)
        {
            repository.Excluir(new Consultas { codm = codm, data = data, hora = hora });
        }
    }
}