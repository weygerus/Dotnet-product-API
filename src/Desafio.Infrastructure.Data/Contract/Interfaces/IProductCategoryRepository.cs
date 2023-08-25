using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;

namespace Desafio.Infrastructure.Data.Contract.Interfaces
{
    public interface IProductCategoryRepository
    {
        public Task<ProductResponseDTO> AddProductCategoryAsync(ProductCategory productCategory);
    }
}
