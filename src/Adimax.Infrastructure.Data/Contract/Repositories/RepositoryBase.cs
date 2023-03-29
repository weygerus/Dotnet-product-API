using adimax.infrastructure.data.contract.interfaces;
using Adimax.Infrastructure.Data.DTO;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IAsyncDisposable where TEntity : class
    {
        //-->Metodos QUERY
        public Task<IEnumerable<object>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //-->Metodos COMMAND
        public TEntity AddAsync(TEntity entity)
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