using Adimax.Domain;
using Adimax.Infrastructure.Data;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adimax.API.Controllers
{
    public class ProductLogController : ControllerBase
    {
        private readonly IProductLogRepository _ProductLogsRepository;
        public ProductLogController(IProductLogRepository productLogs)
        {
            _ProductLogsRepository = productLogs;
        }

        [HttpGet]
        [Route("api/GetProductLog/{Id}")]
        public async Task<object> GetProductLog(int Id)
        {
            //var log = new ProductLog();

            //if (log == null)
            //{
            //    throw new Exception("Nao encontrado");
            //}

            //log = await _ProductLogsRepository.GetById(Id);

            //return log;
            var log = new Product();
            return log;
        }

        [HttpGet]
        [Route("api/GetProductLogs")]
        public async Task<IEnumerable<object>> GetProductLogs()
        {
            var productLogs = await _ProductLogsRepository.GetAll();

            return productLogs;
        }
    }
}
