using Adimax.Domain;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Infrastructure.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        // --> METODOS QUERY
        public Task<IEnumerable<object>> GetAll();

        public Task<Category> GetById(int id);


        // --> METODOS COMMAND
        public ProductResponseDTO AddAsync([FromBody]Category category);

        public void UpdateItem(Category category);

        public Task<Category> DeleteItem(Category category);
    }
}
