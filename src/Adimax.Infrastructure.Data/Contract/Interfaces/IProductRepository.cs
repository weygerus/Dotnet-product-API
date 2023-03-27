using Adimax.Domain;
using adimax.infrastructure.data.contract.interfaces;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        // --> METODOS QUERY
        public Task<IEnumerable<Product>> GetAll();

        public Task<Product> GetById(int id);


        // --> METODOS COMMAND
        public Task<Product> AddAsync(Product product);

        public void UpdateItem(Product newProduct);

        public Task DeleteItem(Product product); 
    }
}
