using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Domain;
using Adimax.Infrastructure.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Infrastructure.Data.Contract.Interfaces
{
    public interface IProductLogRepository : IRepositoryBase<ProductLog>
    {
        public Task<IEnumerable<object>> GetAll();

        public Task<ProductLog> GetById(int Id);
    }
}
