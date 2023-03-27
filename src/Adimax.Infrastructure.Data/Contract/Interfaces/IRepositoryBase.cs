﻿using Adimax.Domain;
using Adimax.Infrastructure.Data.DTO;

namespace adimax.infrastructure.data.contract.interfaces
{
    // interface generica que recebe uma entidade (ex: category, product)
    public interface IRepositoryBase<TEntity>
    {
        public Task<IEnumerable<object>> GetAll();

        public Task<TEntity> GetById(int id, CancellationToken cancellationToken);

        public ProductResponseDTO AddAsync(TEntity entity);

        public void UpdateItem(TEntity entity);

        public Task DeleteItem(TEntity entity);
    }
}