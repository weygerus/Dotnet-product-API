using Adimax.Domain;
using Adimax.Infrastructure.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public class ProductLogRepository : IProductLogRepository
    {
        private readonly DatabaseContext _dbContext;
        public ProductLogRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        //--> METODOS QUERY
        public async Task<IEnumerable<object>> GetAll()
        {
            var ProductLogs = await _dbContext.ProductLogs.ToArrayAsync();
            return ProductLogs;
        }

        public async Task<ProductLog> GetById(int Id)
        {
            var log = await _dbContext.ProductLogs.FindAsync(Id);
            return log;
        }


        //--> METODOS NÃO USADOS
        public ProductResponseDTO AddAsync(ProductLog entity)
        {
            throw new NotImplementedException();
        }
        public Task<ProductLog> DeleteItem(ProductLog entity)
        {
            throw new NotImplementedException();
        }
        public void UpdateItem(ProductLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
