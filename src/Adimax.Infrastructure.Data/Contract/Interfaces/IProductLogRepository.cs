using Adimax.Domain;
using adimax.infrastructure.data.contract.interfaces;
using Adimax.Infrastructure.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
        public interface IProductLogRepository : IRepositoryBase<ProductLog>
        {
            // --> METODOS QUERY
            public Task<IEnumerable<object>> GetAll();

            public Task<ProductLog> GetById(int id, CancellationToken cancellationToken);


            // --> METODOS COMMAND
            public ProductResponseDTO AddAsync(ProductLog productLog);

            public void UpdateItem(ProductLog newProductLog);

            public Task DeleteItem(ProductLog productLog);
        }
}
