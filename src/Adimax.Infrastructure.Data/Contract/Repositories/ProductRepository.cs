using System;
using System.Collections.Generic;
using Adimax.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(int Id)
        {
            return await _dbContext.Products.FindAsync(Id);
        }

        // -->Metodos ALTERACAO
        public async Task<Product> AddAsync(Product product)
        {
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
