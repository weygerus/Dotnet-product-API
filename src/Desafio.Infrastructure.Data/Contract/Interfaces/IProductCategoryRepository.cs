using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Domain;

namespace Desafio.Infrastructure.Data.Contract.Interfaces
{
    public interface IProductCategoryRepository
    {
        public Task<List<string>> GetCategoryNamesListByIdsAsync(List<Category> CategoryList);
        public Task <List<int>> GetProductCategoryByCategoryId(List<int> categoryIdsOfProductCategoriesList, int lastProductInsert);

    }
}
