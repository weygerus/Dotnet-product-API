using Microsoft.AspNetCore.Mvc;
using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.Contract.Repositories;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO.ProductResponses;

namespace Desafio.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductController(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
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

        [HttpGet]
        [Route("api/GetLastInsert")]
        public async Task<ActionResult> GetLastInsert()
        {
            var product = await _productRepository.GetLastInsert();
            return Ok(product);
        }

        /// <summary>
        /// ADICIONAR UM PRODUTO.
        /// </summary>
        [HttpPost]
        [Route("api/InsertProduct")]
        public async Task<object> AddProductAsync([FromBody]Product product)
        {
            try
            {
                if (product is null)
                {
                    return NoContent();
                }

                if (product.ProductCategories.Count == 0)
                {
                    var nullCategoryResponse = "É necessária uma categoria para cadastrar o produto!.";

                    return nullCategoryResponse;
                }

                _productRepository.AddAsync(product);

                List<int> categoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList();
                var lastProductInsert = this.GetLastInsert();

                var CategoryIdsList = await _productCategoryRepository.GetProductCategoryByCategoryId(categoryIds , lastProductInsert.Id);

                var productInsertSucessResponse = new ProductResponseDTO()
                {
                    Id = product.Id,
                    Message = "Produto cadastrado com sucesso!"
                };

                return productInsertSucessResponse;
            }
            catch (Exception addProductException)
            {
                throw addProductException;
            }
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

            var UpdateProductSucessResponsee = new ProductUpdateSucessResponseDTO()
            {
                Id = newProduct.Id,
                Message = "Produto atualizado com sucesso!"
            };

            return UpdateProductSucessResponsee;
        }

        /// <summary>
        /// DELETAR UM PRODUTO.
        /// </summary>
        [HttpDelete]
        [Route("api/DeleteProduct/{Id}")]
        public async Task<ProductDeleteSucessResponseDTO> DeleteProductAsync(int Id)
        {
            var product = await _productRepository.GetById(Id); 
           
            if (product == null)
            {
                throw new Exception("Nao encontrado");
            }
                await _productRepository.DeleteItem(product);

            var deleteProductDeleteSucessResponse = new ProductDeleteSucessResponseDTO()
            {
                Id = product.Id,
                Message = "Produto deletado com sucesso!"
            };

            return deleteProductDeleteSucessResponse;
        }
    }
}