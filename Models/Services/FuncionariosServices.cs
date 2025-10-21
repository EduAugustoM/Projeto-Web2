using Models.Data;
using Models.Entidades;
using Models.ViewModel;

namespace Models.Services
{
    public class FuncionariosServices
    {
        private readonly FuncionariosRepository repository;

        public FuncionariosServices(FuncionariosRepository _repository)
        {
            repository = _repository;
        }

        public List<FuncionariosViewModel> BuscarTodos()
        {
            var listaFuncionarios = repository.BuscarTodos();
            var novaLista = listaFuncionarios.Select(model => new FuncionariosViewModel
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                salario = model.salario,
                CPF = model.CPF
            }).ToList();
            return novaLista;
        }

        public FuncionariosViewModel BuscarFuncionario(int codf = 0)
        {
            if (codf == 0)
            {
                return new FuncionariosViewModel { codf = codf };
            }
            else
            {
                var funcionarioDb = repository.Buscar(codf);
                return new FuncionariosViewModel
                {
                    codf = funcionarioDb.codf,
                    nome = funcionarioDb.nome,
                    idade = funcionarioDb.idade,
                    cidade = funcionarioDb.cidade,
                    salario = funcionarioDb.salario,
                    CPF = funcionarioDb.CPF
                };
            }
        }

        public void SalvarFuncionario(FuncionariosViewModel model)
        {
            Funcionarios funcionario = new Funcionarios
            {
                codf = model.codf,
                nome = model.nome,
                idade = model.idade,
                cidade = model.cidade,
                salario = model.salario,
                CPF = model.CPF
            };

            if (model.codf == 0)
                repository.Salvar(funcionario);
            else
                repository.Atualizar(funcionario);
        }

        public void ExcluirFuncionario(int codf)
        {
            repository.Excluir(new Funcionarios { codf = codf });
        }
    }
}