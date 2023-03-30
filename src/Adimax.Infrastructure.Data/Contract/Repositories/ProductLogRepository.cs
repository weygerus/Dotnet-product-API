using Adimax.Domain;
using Adimax.Infrastructure.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Infrastructure.Data.Contract.Repositories
{
    public class ProductLogRepository
    {
        private readonly DatabaseContext _dbContext;
        public ProductLogRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        // --> METODOS QUERY
        public async Task<IEnumerable<object>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetById(int Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        // --> METODOS COMMAND
        public ProductLog AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async void UpdateItem(Category oldCategory)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItem(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
