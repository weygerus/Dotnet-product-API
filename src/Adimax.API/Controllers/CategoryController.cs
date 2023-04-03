using Microsoft.AspNetCore.Mvc;
using Adimax.Domain;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Infrastructure.Data.DTO;
using System.Net.Http;

namespace Adimax_RestAPI.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly List<Category> CategoryList  = new List<Category>();
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("api/GetCategory/{Id}")]
        public async Task<object> GetCategoryById(int Id)
        {
            var category = await _categoryRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        [Route("api/GetAllCategories")]
        public async Task<object> GetAllCategories()
        {
            var categorys = await _categoryRepository.GetAll();
            return Ok(categorys);
        }

        [HttpPost]
        [Route("api/InsertCategory")]
        public async Task<object> AddCategory([FromBody]Category category)
        {
            if (category == null)
            {
                return NoContent();
            }

            _categoryRepository.AddAsync(category);

            var response = new ProductResponseDTO();
            return response;
        }

        [HttpPut]
        [Route("api/UpdateCategory/{id}")]
        public async Task<object> UpdateCategory(int Id, [FromBody] Category newCategory)
        {
            if (newCategory == null || Id != newCategory.Id)
            {
                return BadRequest();
            }

            var oldCategory = await _categoryRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if (oldCategory == null)
            {
                return NotFound();
            }
            //-->Substituir por foreach
            oldCategory.Name = newCategory.Name;
            oldCategory.Description = newCategory.Description;
            oldCategory.CreatedAt = newCategory.CreatedAt;

            _categoryRepository.UpdateItem(oldCategory);

            return Ok(newCategory);
        }

        [HttpDelete]
        [Route("api/DeleteCategory/{id}")]
        public async Task<object> DeleteCategory(int Id)
        {
            var category = await _categoryRepository.GetById(Id, cancellationToken: CancellationToken.None);

            if (category == null)
            {
                return NoContent();
            }
            _categoryRepository.DeleteItem(category);
            return Ok();
        }
    }
}
