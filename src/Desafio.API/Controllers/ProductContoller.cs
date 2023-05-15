using Microsoft.AspNetCore.Mvc;
using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.Contract.Interfaces;

namespace Desafio.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// CONSULTAR UM PRODUTO POR ID.
        /// </summary>
        [HttpGet]
        [Route("api/GetProduct/{Id}")]
        public async Task<object> GetProductById(int Id)
        {
            var product = await _productRepository.GetById(Id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// CONSULTAR TODOS OS PRODUTOS.
        /// </summary>
        [HttpGet]
        [Route("api/GetAllProducts")]
        public async Task<ActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// ADICIONAR UM PRODUTO.
        /// </summary>
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

        /// <summary>
        /// ATUALIZAR UM PRODUTO.
        /// </summary>
        [HttpPut]
        [Route("api/UpdateProduct/{Id}")]
        public async Task<object> UpdateProductAsync(int Id, [FromBody] Product newProduct)
        {
            newProduct.Id = Id;

            if (newProduct == null || Id != newProduct.Id)
            {
                return BadRequest();
            }

            var oldProduct = await _productRepository.GetById(Id);

            if (oldProduct == null)
            {
                return NotFound();
            }

            oldProduct.Name = newProduct.Name;
            oldProduct.Description = newProduct.Description;
            oldProduct.Price = newProduct.Price;
            oldProduct.CreatedAt = newProduct.CreatedAt;
            oldProduct.HasPendingLogUpdate = newProduct.HasPendingLogUpdate;

            _productRepository.UpdateItem(oldProduct);

            return Ok(newProduct);
        }

        /// <summary>
        /// DELETAR UM PRODUTO.
        /// </summary>
        [HttpDelete]
        [Route("api/DeleteProduct/{Id}")]
        public async Task<Product> DeleteProductAsync(int Id)
        {
            var product = await _productRepository.GetById(Id); 
           
            if (product == null)
            {
                throw new Exception("Nao encontrado");
            }
                await _productRepository.DeleteItem(product);
                return product;
        }
    }
}