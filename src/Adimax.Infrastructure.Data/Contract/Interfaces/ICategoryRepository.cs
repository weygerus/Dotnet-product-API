using Adimax.Domain;
using adimax.infrastructure.data.contract.interfaces;
using Adimax.Infrastructure.Data.DTO;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        // --> METODOS QUERY
        public Task<IEnumerable<object>> GetAll();

        public Task<Category> GetById(int id, CancellationToken cancellationToken);


        // --> METODOS COMMAND
        public ProductResponseDTO AddAsync(Category category);

        public void UpdateItem(Category category);

        public Task DeleteItem(Category category);
    }
}
