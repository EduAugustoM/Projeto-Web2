using Models.ViewModel;
using AspNet_MVC.Models.Entidades;
using Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Services
{
    public class AmbulatoriosServices
    {
        private readonly AmbulatoriosRepository repository;

        public AmbulatoriosServices(AmbulatoriosRepository _repository)
        {
            this.repository = _repository;
        }

        public void SalvarAmbulatorio(AmbulatoriosViewModel model)
        {
            Ambulatorios newModel = new Ambulatorios
            {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
            };
            repository.Salvar(newModel);
        }
        public AmbulatoriosViewModel BuscaAmbulatorio(int nroa = 0)
        {
            AmbulatoriosViewModel model;
            if (nroa == 0)
            {
                model = new AmbulatoriosViewModel { nroa = nroa };
            }
            else
            {
                var modelDB = repository.Buscar(nroa);
                model = new AmbulatoriosViewModel
                {
                    nroa = modelDB.nroa,
                    andar = modelDB.andar,
                    capacidade = modelDB.capacidade
                };
            }
            return model;
        }
        public List<AmbulatoriosViewModel> BuscarTodos()
        {
            var ListaAmbulatorios = repository.BuscarTodos();
            var NovaListaAmbulatorios = ListaAmbulatorios.Select(model => new AmbulatoriosViewModel
            {
                nroa = model.nroa,
                andar = model.andar,
                capacidade = model.capacidade
            }).ToList();
            return NovaListaAmbulatorios;
        }
        public void ExcluirAmbulatorio(int nroa, int andar, int capacidade)
        {
            repository.Excluir(new Ambulatorios { nroa = nroa, andar = andar, capacidade = capacidade });
        }
    }
}