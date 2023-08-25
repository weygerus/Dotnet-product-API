﻿using Microsoft.EntityFrameworkCore;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Domain;

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
