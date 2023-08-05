using Microsoft.AspNetCore.Mvc;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.DTO.Categories;
using Desafio.Domain;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.Extensions;
using System.Linq;

namespace Desafio.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly List<Category> CategoryList = new List<Category>();
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _ConnectionString;

        public CategoryController(ICategoryRepository categoryRepository, IConfiguration Connection)
        {
            _categoryRepository = categoryRepository;
            _ConnectionString = Connection;
        }

        /// <summary>
        /// CONSULTAR UMA CATEGORIA POR ID.
        /// </summary>
        [HttpGet]
        [Route("api/GetCategory/{Id}")]
        public async Task<object> GetCategoryById(int Id)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=ADIMAX_API;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False";
            IDbConnection connection = new SqlConnection(connectionString);

            string selectCategories = "SELECT * FROM CATEGORY WHERE Id = @id";
            string selectProductsIdsOnThisCategory = @"
                    SELECT pc.PRODUCT_ID
                    FROM PRODUCT_CATEGORY pc
                    INNER JOIN PRODUCT p ON p.ID = pc.PRODUCT_ID
                    WHERE pc.CATEGORY_ID = @Id";

            var param = new { id = Id };

            Category category = connection.QueryFirstOrDefault<Category>(selectCategories, param);

            if (category == null)
            {
                return NoContent();
            }

            List<ProductCategory> products = connection.Query<ProductCategory>(selectProductsIdsOnThisCategory, param).ToList();

            foreach (var productId in products)
            {
                List<ProductCategory> ids = new List<ProductCategory>();

                ids.Add(productId);

                category.ProductCategories = ids;
            }


            return Ok(category);
        }

        /// <summary>
        /// CONSULTAR UMA CATEGORIA POR ID.
        /// </summary>
        [HttpGet]
        [Route("api/GetAllCategories")]
        public async Task<object> GetCategoriesAsync()
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=ADIMAX_API;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False";
            IDbConnection connection = new SqlConnection(connectionString);

            string selectCategories = "SELECT * FROM CATEGORY";

            var categories = connection.QueryFirstOrDefault<Category>(selectCategories);

            if (categories == null)
            {
                return NoContent();
            }

            return Ok(categories);
        }

        /// <summary>
        /// CONSULTAR UMA CATEGORIA POR ID.
        /// </summary>
        [HttpPost]
        [Route("api/InsertCategory")]
        public async Task<object> AddCategoryAsync([FromBody]Category category)
        {
            if (category == null)
            {
                throw new Exception("Dados da categoria não encontrados");
            }

            _categoryRepository.AddAsync(category);

            var CategoryInsertSucessResponse = new CategoryResponseDTO()
            {
                Id = category.Id,
                Message = "Categoria cadastrada com sucesso!"
            };

            return CategoryInsertSucessResponse;
        }

        /// <summary>
        /// CONSULTAR UMA CATEGORIA POR ID.
        /// </summary>
        [HttpPut]
        [Route("api/UpdateCategory/{Id}")]
        public async Task<object> UpdateCategoryAsync(int Id, [FromBody]Category newCategory)
        {
            newCategory.Id = Id;
            
            if (newCategory == null || Id != newCategory.Id)
            {
                return BadRequest();
            }

            if (newCategory.ProductCategories is null)
            {
                var nullCategoryResponse = "Produto sem categoria cadastrada.";

                return nullCategoryResponse;
            }

            var oldCategory = await _categoryRepository.GetById(Id);

            if (oldCategory == null)
            {
                return NotFound();
            }
            //-->Substituir por foreach
            oldCategory.Name = newCategory.Name;
            oldCategory.Description = newCategory.Description;
            oldCategory.UpdateAt = newCategory.UpdateAt;

            _categoryRepository.UpdateItem(oldCategory);

            var categoryUpdateSucessResponse = new CategoryUpdateSucessResponseDTO()
            {
                Id = newCategory.Id,
                Message = "Categoria atualizada com sucesso!"
            };

            return categoryUpdateSucessResponse;
        }

        /// <summary>
        /// CONSULTAR UMA CATEGORIA POR ID.
        /// </summary>
        [HttpDelete]
        [Route("api/DeleteCategory/{Id}")]
        public async Task<Category> DeleteCategoryAsync(int Id)
        {
            var category = await _categoryRepository.GetById(Id);

            if (category == null)
            {
                throw new Exception("Nao encontrada");
            }
                await _categoryRepository.DeleteItem(category);
                return category;
        }
    }
}
