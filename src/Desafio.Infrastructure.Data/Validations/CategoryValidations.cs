using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.Contract.Validations;
using Desafio.Infrastructure.Data.Contract.Interfaces;

namespace Desafio.Infrastructure.Data.Validations
{
    public class CategoryValidations : ICategoryValidations
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryValidations(ICategoryRepository categoryRepository) 
        {
            _CategoryRepository = categoryRepository;
        }

        public async Task<categoryValidationResponseDTO> ValidateAvailability(Category categoyToValidate)
        {
            var response = new categoryValidationResponseDTO();

            if (!await this.ValidateName(categoyToValidate.Name))
            {
                response.IsAvailable = false;
                response.Message = "Nome da categoria já existe.";
            }

            response.IsAvailable = true;

            return response;
        }

        public async Task<bool> ValidateName(string nameToCompare)
        {
            var existingName = await _CategoryRepository.GetCategoryByName(nameToCompare);

            if (existingName is null)
            {
                return true;
            }

            return false;
        }
    }
}
