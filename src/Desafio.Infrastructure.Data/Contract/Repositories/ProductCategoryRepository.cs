using Desafio.Domain;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;

namespace Desafio.Infrastructure.Data.Contract.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DatabaseContext _DbContext;

        public ProductCategoryRepository(DatabaseContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<ProductResponseDTO> AddProductCategoryAsync(ProductCategory productCategory)
        {
            var res = new ProductResponseDTO();

            await _DbContext.ProductCategories.AddRangeAsync(productCategory);

            return res;
        }
    }
}
