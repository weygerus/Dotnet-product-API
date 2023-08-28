using Desafio.Domain;

namespace Desafio.Infrastructure.Data.Contract.Interfaces
{
    public interface IProductLogRepository : IRepositoryBase<ProductLog>
    {
        public Task<IEnumerable<object>> GetAll();

        public Task<ProductLog> GetById(int Id);
    }
}
