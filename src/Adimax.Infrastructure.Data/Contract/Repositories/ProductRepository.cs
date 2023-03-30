using System;
using System.Collections.Generic;
using Adimax.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Adimax.Infrastructure.Data.DTO;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _dbContext;

        public ProductRepository(DatabaseContext dbContext) 
        {
            _dbContext = dbContext;
        }

        // -->Metodos QUERY
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

        public async Task<Product> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.FindAsync(Id);
        }

         //-->Metodos ALTERACAO
        public Product AddAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return product;
        }

        public void UpdateItem(Product oldProduct)
        {
            _dbContext.Products.Update(oldProduct);
            _dbContext.SaveChanges();
        }

        public async Task DeleteItem(Product product)
        {
            var DeleteProduct = _dbContext.Products.Find(product);

            if(DeleteProduct == null)
            {
                throw new Exception("Produto não encontrado");
            }
            _dbContext.Products.Remove(DeleteProduct);
            _dbContext.SaveChanges();
        }
    }
}
