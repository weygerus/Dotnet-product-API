using System;
using System.Collections.Generic;
using Adimax.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adimax.Infrastructure.Data.DTO;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _dbContext;
        public CategoryRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // -->Metodos QUERY
        public async Task<IEnumerable<object>> GetAll()
        {
            var categories = await _dbContext.Categories.Include(c => c.ProductCategories)
                                                        .ThenInclude(pc => pc.ProductIn)
                                                        .ToListAsync();

            return categories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Description,
                Products = c.ProductCategories.Select(pc => pc.ProductIn.Name),
                c.CreatedAt,
                c.UpdateAt
            });
           // return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int Id, CancellationToken cancellationToken)
        {
              return await _dbContext.Categories.FindAsync(Id);

            // --> Incluir obejto de produto
            //var categoryWithProducts = await _dbContext.Categories.Include(c => c.ProductCategories)
            //                                                      .ThenInclude(pc => pc.ProductIn);
                                                                
            //return category.Select(c => new
            //{
            //    c.Id,
            //    c.Name,
            //    c.Description,
            //    c.Products = c.ProductCategories.Select(c => c.ProductIn.Name),
            //    c.CreatedAt,
            //    c.UpdateAt 
            //});
        }

        // -->Metodos ALTERACAO
        public Category AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return category;
        }

        public async void UpdateItem(Category oldCategory)
        {
            _dbContext.Categories.Update(oldCategory);
            _dbContext.SaveChanges();
        }

        public async Task DeleteItem(Category category)
        {
            var DeleteProduct = _dbContext.Categories.Find(category);

            if (DeleteProduct == null)
            {
                throw new Exception("Produto não encontrado");
            }
            _dbContext.Categories.Remove(DeleteProduct);
            _dbContext.SaveChanges();
        }
    }
}
