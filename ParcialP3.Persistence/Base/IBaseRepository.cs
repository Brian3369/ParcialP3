namespace ParcialP3.Persistence.Base
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetEntityBy(int Id);
        Task<bool> RemoveById(int id);
        Task<bool> Save(T entity);
        Task<bool> Update(T entity);
    }
}
