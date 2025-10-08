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

            //repository.Atualizar(newModel);
        }

        public ConsultasViewModel BuscaConsulta(int codm = 0, DateTime? data = null, TimeSpan? hora = null)
        {
            ConsultasViewModel model;
            if (codm == 0)
            {
                model = new ConsultasViewModel { codm = codm };
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
            }
            else
            {
                var modelDB = repository.Buscar(codm, data.GetValueOrDefault(), hora.GetValueOrDefault());
                model = new ConsultasViewModel
                {
                    codm = modelDB.codm,
                    codp = modelDB.codp,
                    data = modelDB.data,
                    hora = modelDB.hora
                };

                model.Pacientes = repositoryPacientes.BuscarTodos().Select(
                   p => new SelectListItem
                   {
                       Value = p.codp.ToString(),
                       Text = p.nome
                   }
               ).ToList();
                model.Medicos = repositoryMedicos.BuscarTodos().Select(
                    item => new SelectListItem
                    {
                        Value = item.codm.ToString(),
                        Text = item.nome
                    }
                ).ToList();
            }

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