using Microsoft.AspNetCore.Mvc;

using Desafio.Domain;
using Desafio.Infrastructure.Data.Contract.Interfaces;

namespace Desafio.API.Controllers
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
