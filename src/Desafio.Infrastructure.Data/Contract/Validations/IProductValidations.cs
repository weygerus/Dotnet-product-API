using Desafio.Domain;
using Desafio.Infrastructure.Data.DTO;
using Desafio.Infrastructure.Data.Validations;

namespace Desafio.Infrastructure.Data.Contract.Validations
{
    public interface IProductValidations : IValidationsBase<Product>
    {
        public Task<ProductValidationResponseDTO> ValidateAvailability(Product productToValidate);
    }
}
