namespace TestTask.Repositories.Infrastructure
{
    public interface IBaseRepository<TEntity, TFilter>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(TFilter filter);
        Task<TEntity?> FindAsync(long id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
