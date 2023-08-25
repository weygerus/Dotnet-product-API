using Desafio.Domain;
using Desafio.Infrastructure.Data.Contract.Interfaces;

namespace Desafio.API.Validations
{
    public class CategoryValidations
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryValidations(ICategoryRepository categoryRepository) 
        {
            _CategoryRepository = categoryRepository;
        }

        public bool ValidateAvailability(Category categoyforCompareName)
        {
            if (this.ValidateName(categoyforCompareName.Name))
            {
                return true;
            }

            return false;
        }

        public bool ValidateName(string nameToCompare)
        {
            var name = _CategoryRepository.GetCategoryByName(nameToCompare);

            if (name == null)
            {
                return true;
            }

            return false;
        }
    }
}
