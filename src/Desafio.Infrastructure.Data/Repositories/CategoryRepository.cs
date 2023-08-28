using Microsoft.EntityFrameworkCore;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Domain;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace Desafio.Infrastructure.Data.Contract.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IProductRepository _ProductRepository;
        private readonly DatabaseContext _dbContext;

        public CategoryRepository(DatabaseContext dbContext, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _ProductRepository = productRepository;
        }

        public async Task<List<string>> GetCategoryNamesListByIdsAsync(List<Category> CategoryList)
        {
            var categoryList = new List<ProductCategory>();

            var stringlist = new List<string>();

            foreach (var categoryName in CategoryList)
            {

               // categoryList = await _dbContext.Categories.Where(c => c.Na).ToList();
            }

            return stringlist;
        }

        public async Task<Category> GetCategoryByName(string category)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=ADIMAX_API;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False";

            using (var connection = new SqlConnection(connectionString))
            {
                var categoryResult = new Category();

                var param = category;

                var sqlQuery = "SELECT * FROM CATEGORY WHERE Name = @Param";

                categoryResult = connection.QueryFirstOrDefault<Category>(sqlQuery, new { Param = param });

                return categoryResult;
            }
        }

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
        }


        public async Task<Category> GetById(int Id)
        {
            var category = await _dbContext.Categories.FindAsync(Id);

            if (category is null)
            {
                var errorCategory = new Category();

                return errorCategory;
            }

            var productCategories = _dbContext.ProductCategories.Where(pc => pc.CategoryId == Id).ToList();

            var productNamesIntoCategoryList = new List<string>();

            foreach (var productCategory in productCategories)
            {
                var productId = productCategory.ProductId;

                productNamesIntoCategoryList.Add(await _ProductRepository.GetNameProductById(productId));
            }

            category.ProductsOnCategory = productNamesIntoCategoryList;

            return category;
        }

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
