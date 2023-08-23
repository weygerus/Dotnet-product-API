using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Desafio.Domain;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Microsoft.Data.SqlClient;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Domain;

namespace Desafio.Infrastructure.Data.Contract.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DatabaseContext _DbContext;

        public ProductCategoryRepository(
            DatabaseContext dbContext
            )
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
