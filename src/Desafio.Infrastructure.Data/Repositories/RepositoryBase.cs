using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;

namespace Desafio.Infrastructure.Data.Contract.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IAsyncDisposable where TEntity : class
    {
        public Task<IEnumerable<object>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ProductResponseDTO AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> DeleteItem(TEntity entity)
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