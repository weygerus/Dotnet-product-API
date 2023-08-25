using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Infrastructure.Data.Contract.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        public Category GetCategoryByName(string category);

        public Task<IEnumerable<object>> GetAll();

        public Task<Category> GetById(int id);

        public ProductResponseDTO AddAsync([FromBody]Category category);

        public void UpdateItem(Category category);

        public Task<Category> DeleteItem(Category category);

        public Task<List<string>> GetCategoryNamesListByIdsAsync(List<Category> CategoryList);
    }
}
