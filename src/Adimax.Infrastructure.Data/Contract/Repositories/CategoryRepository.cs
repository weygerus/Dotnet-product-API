using System;
using System.Collections.Generic;
using Adimax.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int Id)
        {
            return await _dbContext.Categories.FindAsync(Id);
        }

        // -->Metodos ALTERACAO
        public async Task<Category> AddAsync(Category category)
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
