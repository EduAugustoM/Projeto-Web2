using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;

namespace Models.Services
{
    public class MedicosServices
    {
        private readonly MedicosRepository repository;
        private readonly AmbulatoriosRepository ambulatoriosRepository; 

        public MedicosServices(MedicosRepository _repository, AmbulatoriosRepository _ambulatoriosRepository)
        {
            this.repository = _repository;
            this.ambulatoriosRepository = _ambulatoriosRepository;
        }

        public List<MedicosViewModel> BuscarTodos()
        {
            var listaMedicos = repository.BuscarTodos();
            var novaLista = listaMedicos.Select(model => new MedicosViewModel
            {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                CPF = model.CPF,
                cidade = model.cidade,
                nroa = model.nroa
            }).ToList();
            return novaLista;
        }

        public MedicosViewModel BuscarMedico(int codm = 0)
        {
            MedicosViewModel model;
            if (codm == 0)
            {
                model = new MedicosViewModel { codm = codm };
            }
            else
            {
                var medicoDb = repository.Buscar(codm);
                model = new MedicosViewModel
                {
                    codm = medicoDb.codm,
                    nome = medicoDb.nome,
                    idade = medicoDb.idade,
                    especialidade = medicoDb.especialidade,
                    CPF = medicoDb.CPF,
                    cidade = medicoDb.cidade,
                    nroa = medicoDb.nroa
                };
            }
            
            model.Ambulatorios = ambulatoriosRepository.BuscarTodos().Select(
                a => new SelectListItem
                {
                    Value = a.nroa.ToString(),
                    Text = a.nroa.ToString() 
                }
            ).ToList();

            return model;
        }

        public void SalvarMedico(MedicosViewModel model)
        {
            Medicos medico = new Medicos
            {
                codm = model.codm,
                nome = model.nome,
                idade = model.idade,
                especialidade = model.especialidade,
                CPF = model.CPF,
                cidade = model.cidade,
                nroa = model.nroa
            };

            if (model.codm == 0)
            {
                repository.Salvar(medico);
            }
            else
            {
                repository.Atualizar(medico);
            }
        }

        public void ExcluirMedico(int codm)
        {
            repository.Excluir(new Medicos { codm = codm });
        }
    }
}