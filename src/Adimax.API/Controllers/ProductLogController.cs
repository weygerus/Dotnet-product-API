using Microsoft.AspNetCore.Mvc;
using Adimax.Domain;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Infrastructure.Data.DTO;
using System.Net.Http;

namespace Adimax.API.Controllers
{
    public class ProductLogController : ControllerBase
    {
        private readonly List<Product> productLogList = new List<Product>();
        private readonly IProductLogRepository _productLogRepository;

        public ProductLogController(IProductLogRepository productLogRepository)
        {
            _productLogRepository = productLogRepository;
        }

        [HttpGet]
        [Route("api/GetProduct/{Id}")]
        public async Task<object> GetProductLog(int Id)
        {
            var product = await _productLogRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("api/GetAllProducts")]
        public async Task<ActionResult> GetAllProductLogs()
        {
            var productLogs = await _productLogRepository.GetAll();
            return Ok(productLogs);
        }

        [HttpPost]
        [Route("api/InsertProduct")]
        public async Task<object> AddProductLog([FromBody] ProductLog productLogs)
        {
            if (productLogs == null)
            {
                return NoContent();
            }

            _productLogRepository.AddAsync(productLogs);

            var response = new ProductResponseDTO();
            return response;
        }

        [HttpPut]
        [Route("api/UpdateProduct/{id}")]
        public async Task<object> UpdateProductLog(int Id, [FromBody]ProductLog newProductLog)
        {
            if (newProductLog == null || Id != newProductLog.Id)
            {
                return BadRequest();
            }

            var oldProductLog = await _productLogRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if (oldProductLog == null)
            {
                return NotFound();
            }
            //-->Substituir por foreach
            newProductLog.UpdatedAt = newProductLog.UpdatedAt;
            newProductLog.ProductJson = newProductLog.ProductJson;

            _productLogRepository.UpdateItem(oldProductLog);

            return Ok(newProductLog);
        }

        [HttpDelete]
        [Route("api/DeleteProduct")]
        public async Task<object> DeleteProductLog(int Id)
        {
            var productLog = await _productLogRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if (productLog == null)
            {
                return NoContent();
            }
            _productLogRepository.DeleteItem(productLog);
            return Ok();
        }
    }
}
