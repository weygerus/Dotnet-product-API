using Microsoft.AspNetCore.Mvc;
using Adimax.Domain;
using Adimax.Infrastructure.Data.Contract.Interfaces;
using Adimax.Infrastructure.Data.DTO;
using Adimax.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Adimax_RestAPI.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly List<Category> CategoryList = new List<Category>();
        private readonly ICategoryRepository _categoryRepository;
        private readonly DatabaseContext _dbContext;

        public CategoryController(ICategoryRepository categoryRepository, DatabaseContext dbContext)
        {
            _categoryRepository = categoryRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// CONSULTAR UMA CATEGORIA POR ID.
        /// </summary>
        [HttpGet]
        [Route("api/GetCategory/{Id}")]
        public async Task<object> GetCategoryById(int Id)
        {
            var category = await _categoryRepository.GetById(Id);

            if (category == null)
            {
                return NotFound();
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
            var categorys = await _categoryRepository.GetAll();
            return Ok(categorys);
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
                throw new Exception("Nao encontrada");
            }

            _categoryRepository.AddAsync(category);

            var response = new Category();
            return response;
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

            return Ok(newCategory);
        }

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
