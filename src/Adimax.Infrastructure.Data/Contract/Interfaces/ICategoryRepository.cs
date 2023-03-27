using Adimax.Domain;
using adimax.infrastructure.data.contract.interfaces;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        // --> METODOS QUERY
        public Task<IEnumerable<Category>> GetAll();

        public Task<Category> GetById(int id);


        // --> METODOS COMMAND
        public Task<Category> AddAsync(Category category);

        public void UpdateItem(Category category);

        public Task DeleteItem(Category category);
    }
}
