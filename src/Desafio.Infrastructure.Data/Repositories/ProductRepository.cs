using Microsoft.EntityFrameworkCore;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;
using System.Data.SqlClient;
using Desafio.Domain;
using Dapper;

namespace Desafio.Infrastructure.Data.Contract.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _dbContext;

        public ProductRepository(DatabaseContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<object>> GetAll()
        {
            var products = await _dbContext.Products.Include(p => p.ProductCategories)
                                                    .ThenInclude(pc => pc.CategoryIn)
                                                    .ToListAsync();

            return products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                Categories = p.ProductCategories.Select(pc => pc.CategoryIn.Name),
                p.CreatedAt,
                p.HasPendingLogUpdate
            }) ;
        }

        public async Task<Product> GetById(int Id)
        {
            return await _dbContext.Products.FindAsync(Id);
        }

        public async Task<Product> GetProductByName(string product)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=ADIMAX_API;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False";

            using (var connection = new SqlConnection(connectionString))
            {
                var productResult = new Product();

                var param = product;

                var sqlQuery = "SELECT * FROM CATEGORY WHERE Name = @Param";

                productResult = connection.QueryFirstOrDefault<Product>(sqlQuery, new { Param = param });

                return productResult;
            }
        }

        public async Task<string> GetNameProductById(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product is null)
            {
                return string.Empty;
            }

            var productName = product.Name;

            return productName;
        }

        public ProductResponseDTO AddAsync(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                _dbContext.Dispose();

                var res = new ProductResponseDTO();

                if (res.Id < 0)
                {
                    throw new Exception("Não foi possivél cadastrar o produto!");
                }

                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetLastInsert()
        {
            var lastInsert = _dbContext.Products.FirstOrDefault();

            return lastInsert.Id;
        }

        public void UpdateItem(Product oldProduct)
        {
            _dbContext.Products.Update(oldProduct);
            _dbContext.SaveChanges();
            _dbContext.Dispose();
        }

        public async Task<Product> DeleteItem(Product product)
        { 
            if(product == null)
            {
                throw new Exception("Produto não encontrado");
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}
