using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.Validations;

namespace Desafio.Infrastructure.Data.Contract.Validations
{
    public interface ICategoryValidations : IValidationsBase<CategoryValidations>
    {
        public Task<categoryValidationResponseDTO> ValidateAvailability(Category categoyToValidate);
    }
}
