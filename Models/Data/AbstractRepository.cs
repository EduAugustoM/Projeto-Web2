public abstract class AbstractRepository<T>
{
    public abstract void Salvar(T model);
    public abstract void Atualizar(T model);
    public abstract void Excluir(T model);
    public abstract T Buscar(int id);
    public abstract List<T> BuscarTodos();

}