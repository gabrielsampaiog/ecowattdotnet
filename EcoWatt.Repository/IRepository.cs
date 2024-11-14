namespace EcoWatt.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task Add(T entity, int choice);

        Task Update(int id, T entity);

        Task Delete(int id);
    }
}
