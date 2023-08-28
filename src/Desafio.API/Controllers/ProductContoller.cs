using Microsoft.AspNetCore.Mvc;
using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.Data.Contract.Validations;

namespace Desafio.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        private readonly IProductValidations _ProductValidations;

        public ProductController(
            IProductValidations productValidations,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
            _ProductValidations = productValidations;
        }

        [HttpGet]
        [Route("api/GetProduct/{Id}")]
        public async Task<IActionResult> GetProductById(int Id)
        {
            var product = await _productRepository.GetById(Id);

            if(product == null)
            {
                return NoContent();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("api/GetAllProducts")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetAll();

            return Ok(products);
        }

        [HttpPost]
        [Route("api/InsertProduct")]
        public async Task<object> AddProductAsync([FromBody]Product product)
        {
            var validationResponse = await _ProductValidations.ValidateAvailability(product);

            if (!validationResponse.IsAvailable)
            {
                return BadRequest($"Erro ao cadastrar: {validationResponse.Message}");
            }

            _productRepository.AddAsync(product);

            var productInsertSucessResponse = new ProductResponseDTO()
            {
                Id = product.Id,
                Message = "Produto cadastrado com sucesso!"
            };

            return Ok(productInsertSucessResponse);
        }

        [HttpPut]
        [Route("api/UpdateProduct/{Id}")]
        public async Task<object> UpdateProductAsync(int Id, [FromBody] Product newProduct)
        {
            var productCompare = this.GetProductById(Id);

            if (productCompare.Id.ToString() == string.Empty)
            {
                var errorMessage = "Produto não encontrado no sistema!";

                var productUpdateResponse = new ProductResponseDTO()
                {
                    Id = Id,
                    Message = errorMessage
                };

                return productUpdateResponse;
            }

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

            var UpdateProductSucessResponsee = new ProductResponseDTO()
            {
                Id = newProduct.Id,
                Message = "Produto atualizado com sucesso!"
            };

            return UpdateProductSucessResponsee;
        }

        [HttpDelete]
        [Route("api/DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProductAsync(int Id)
        {
            var product = await _productRepository.GetById(Id); 
           
            if (product is null)
            {
                return NoContent();
            }

            await _productRepository.DeleteItem(product);

            var DeleteSucessResponse = new ProductResponseDTO()
            {
                Id = product.Id,
                Message = "Produto deletado com sucesso!"
            };

            return Ok(DeleteSucessResponse);
        }
    }
}