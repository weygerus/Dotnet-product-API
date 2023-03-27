using Adimax.Domain;

namespace adimax.infrastructure.data.contract.interfaces
{
    // interface generica que recebe uma entidade (ex: category, product)
    public interface IRepositoryBase<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAll();

        public Task<TEntity> GetById(int id);

        public Task<TEntity> AddAsync(TEntity entity);

        public void UpdateItem(TEntity entity);

        public Task DeleteItem(TEntity entity);
    }
}