using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;

namespace Desafio.Infrastructure.Data.Contract.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        public Task<IEnumerable<object>> GetAll();

        public Task<Product> GetById(int id);

        public int GetLastInsert();

        public ProductResponseDTO AddAsync(Product product);

        public void UpdateItem(Product newProduct);

        public Task<Product> DeleteItem(Product product); 
    }
}
