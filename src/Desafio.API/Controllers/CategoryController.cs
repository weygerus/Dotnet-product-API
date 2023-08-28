using Microsoft.AspNetCore.Mvc;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Domain;
using Desafio.Infrastructure.Data.Contract.Validations;

namespace Desafio.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryValidations _CategoryValidations;

        public CategoryController(
            ICategoryValidations categoryValidations,
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _CategoryValidations = categoryValidations;
        }

        [HttpGet]
        [Route("api/GetCategory/{Id}")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            var category = await _categoryRepository.GetById(Id);

            if (category.Id is 0)
            {
                return NoContent();
            }

            category.ProductCategories = null; // Evita execeção referencia ciclíca do objeto.

            return Ok(category);
        }

        [HttpGet]
        [Route("api/GetAllCategories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll();

            if (categories == null)
            {
                return NoContent();
            }

            return Ok(categories);
        }
  
        [HttpPost]
        [Route("api/InsertCategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody]Category category)
        {
            if (category is null)
            {
                return BadRequest("Dados da categoria não encontrados");
            }

            var validationResponse = await _CategoryValidations.ValidateAvailability(category);

            if (!validationResponse.IsAvailable)
            {
                return BadRequest($"Erro ao cadastrar categoria! {validationResponse.Message}");             
            }

            _categoryRepository.AddAsync(category);

            var categoryInsertSucessResponse = new CategoryResponseDTO()
            {
                Id = category.Id,
                Message = "Categoria cadastrada com sucesso!"
            };

            return Ok(categoryInsertSucessResponse);
        }

        [HttpPut]
        [Route("api/UpdateCategory/{Id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int Id, [FromBody]Category UpdatedCategory)
        {
            UpdatedCategory.Id = Id;
            
            if (UpdatedCategory is null)
            {
                return BadRequest();
            }

            var oldCategory = await _categoryRepository.GetById(Id);

            if (oldCategory == null)
            {
                return BadRequest("Erro: Categoria informada não existe!");
            }

            //-->Substituir por foreach
            oldCategory.Name = UpdatedCategory.Name;
            oldCategory.Description = UpdatedCategory.Description;
            oldCategory.UpdateAt = UpdatedCategory.UpdateAt;

            _categoryRepository.UpdateItem(oldCategory);

            var categoryUpdateSucessResponse = new CategoryResponseDTO()
            {
                Id = UpdatedCategory.Id,
                Message = "Categoria atualizada com sucesso!"
            };

            return Ok(categoryUpdateSucessResponse);
        }

        [HttpDelete]
        [Route("api/DeleteCategory/{Id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int Id)
        {
            var category = await _categoryRepository.GetById(Id);

            if (category is null)
            {
                return NoContent();
            }

            await _categoryRepository.DeleteItem(category);

            var categoryDeleteSucessResponse = new CategoryResponseDTO()
            {
                Id = category.Id,
                Message = "Categoria excluida com sucesso!"
            };

            return Ok(categoryDeleteSucessResponse);
        }
    }
}