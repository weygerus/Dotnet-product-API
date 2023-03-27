using adimax.infrastructure.data.contract.interfaces;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IAsyncDisposable where TEntity : class
    {
        //-->Metodos QUERY
        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        //-->Metodos COMMAND
        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}