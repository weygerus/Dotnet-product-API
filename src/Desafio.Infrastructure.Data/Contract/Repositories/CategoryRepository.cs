using Microsoft.EntityFrameworkCore;

using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Domain;

namespace Desafio.Infrastructure.Data.Contract.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _dbContext;
        public CategoryRepository(
            DatabaseContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        // -->Metodos QUERY
        public async Task<IEnumerable<object>> GetAll()
        {
            var categories = await _dbContext.Categories.Include(c => c.ProductCategories)
                                                        .ThenInclude(pc => pc.ProductIn)
                                                        .ToListAsync();

            return categories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Description,
                Products = c.ProductCategories.Select(pc => pc.ProductIn.Name),
                c.CreatedAt,
                c.UpdateAt
            });
           // return await _dbContext.Categories.ToListAsync();
        }

        public async Task<List<string>> GetCategoryNamesListByIdsAsync(List<Category> CategoryList)
        {
            var categoryNameList = new List<string>();

            foreach (var categoryName in CategoryList)
            {
                var categoryObject = await _dbContext.Categories.FindAsync(categoryName);

                categoryNameList.Add(categoryObject.Name);
            }

            return categoryNameList;
        }

        public async Task<Category> GetById(int Id)
        {
            return await _dbContext.Categories.FindAsync(Id);

            //var categoryWithProducts = await _dbContext.Categories.Include(c => c.ProductCategories)
            //                                                      .ThenInclude(pc => pc.ProductIn);
                                                                
            //return category.Select(c => new
            //{
            //    c.Id,
            //    c.Name,
            //    c.Description,
            //    c.Products = c.ProductCategories.Select(c => c.ProductIn.Name),
            //    c.CreatedAt,
            //    c.UpdateAt 
            //});
        }

        // -->Metodos ALTERACAO
        public ProductResponseDTO AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            _dbContext.Dispose();

            var res = new ProductResponseDTO();
            return res;
        }

        public async void UpdateItem(Category oldCategory)
        {
            _dbContext.Categories.Update(oldCategory);
            await _dbContext.SaveChangesAsync();
            _dbContext.Dispose();
        }

        public async Task<Category> DeleteItem(Category category)
        {
            if (category == null)
            {
                throw new Exception("nao encontrada");
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            _dbContext.Dispose();

            return category;
        }
    }
}
