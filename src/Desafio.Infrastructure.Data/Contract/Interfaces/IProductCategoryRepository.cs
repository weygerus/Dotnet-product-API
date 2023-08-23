using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;

namespace Desafio.Infrastructure.Data.Contract.Interfaces
{
    public interface IProductCategoryRepository
    {
        public Task<ProductResponseDTO> AddProductCategoryAsync(ProductCategory productCategory);
    }
}
