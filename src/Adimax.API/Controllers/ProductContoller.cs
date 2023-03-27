using Microsoft.AspNetCore.Mvc;
using Adimax.Domain;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Infrastructure.Data.DTO;
using System.Net.Http;

namespace Adimax_RestAPI.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly List<Product> productsList = new List<Product>();
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("api/GetProduct/{Id}")]
        public async Task<object> GetProductById(int Id)
        {
            var product = await _productRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("api/GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpPost]
        [Route("api/InsertProduct")]
        public async Task<object> AddProductAsync([FromBody]Product product)
        {
            if (product == null)
            {
                return NoContent();
            }

            _productRepository.AddAsync(product);

            var response = new ProductResponseDTO();
            return response;
        }

        [HttpPut]
        [Route("api/UpdateProduct/{id}")]
        public async Task<object> UpdateAsync(int Id, [FromBody]Product newProduct)
        {
            if (newProduct == null || Id != newProduct.Id)
            {
                return BadRequest();
            }

            var oldProduct = await _productRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if (oldProduct == null)
            {
                return NotFound();
            }
                //-->Substituir por foreach
                oldProduct.Name = newProduct.Name;
                oldProduct.Description = newProduct.Description;
                oldProduct.Price = newProduct.Price;
                oldProduct.CreatedAt = newProduct.CreatedAt;
                oldProduct.HasPendingLogUpdate = newProduct.HasPendingLogUpdate;

                _productRepository.UpdateItem(oldProduct);

                return Ok(newProduct);
        }

        [HttpDelete]
        [Route("api/DeleteProduct")]
        public async Task<object> DeleteProduct(int Id)
        {
            var product = await _productRepository.GetById(Id, cancellationToken: CancellationToken.None); 
           
            if(product == null)
            {
                return NoContent();
            }
                _productRepository.DeleteItem(product);
                return Ok();
        }
    }
}