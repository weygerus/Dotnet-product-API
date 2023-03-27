﻿using System;
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
            var category = await _dbContext.Categories.FindAsync(Id);

            return category;
        }

        // -->Metodos ALTERACAO
        public ProductResponseDTO AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            var res = new ProductResponseDTO();
            return res;
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
