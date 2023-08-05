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

namespace Desafio.Infrastructure.Data.Contract.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DatabaseContext _DbContext;
        private readonly IConfiguration _Configuration;

        public ProductCategoryRepository(
            DatabaseContext dbContext,
            IConfiguration configuration
            )
        {
            _DbContext = dbContext;
            _Configuration = configuration;
        }

        public async Task<List<string>> GetCategoryNamesListByIdsAsync(List<Category> CategoryList)
        {
            var categoryNameList = new List<string>();

            foreach (var category in CategoryList)
            {
                var categoryObject = await _DbContext.Categories.FindAsync(category);

                categoryNameList.Add(categoryObject.Name);
            }

            return categoryNameList;
        }

        public async Task<List<int>> GetProductCategoryByCategoryId(List<int> categoryIdsOfProductCategoriesList, int lastProductInsert)
        {
            //var connection = new SqlConnection(_Configuration.GetConnectionString("Server=localhost\\SQLEXPRESS;Database=ADIMAX_API;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False"));

            // Get das categorias
            foreach (var productCategory in categoryIdsOfProductCategoriesList)
            {
                List<int> productCategories = new List<int>();

                productCategories.Add(productCategory);
            }
                
            var dapperCategoryQuery = _DbContext.ProductCategories.Include();

            var dapperCategoryParams = new {ID = categoryIdsOfProductCategoriesList};

            IEnumerable<int> categoryIdsReturnList = _DbContext.Query<int>(dapperCategoryQuery, dapperCategoryParams);

            List<int> categoryReturnId = new List<int>();

            foreach (var categoriesIdReturnIteration in categoryIdsReturnList)
            {
                categoryReturnId.Add(categoriesIdReturnIteration);
            }

            return categoryReturnId;
        }
    }
}
